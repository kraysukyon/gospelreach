window.firebaseAuth = {
    register: async function (account) {
        try {
            const auth = firebase.auth();
            const userCredential = await auth.createUserWithEmailAndPassword(account.email, account.password);
            const uid = userCredential.user.uid;

            const db = firebase.firestore();
            //await db.collection("Accounts").add(user);
            await db.collection("Users").doc(uid).set({
                email: account.email,
                firstName: account.firstName,
                middleName: account.middleName,
                lastName: account.lastName,
                role: account.role,
                status: account.status,
                attempts: 5,
                lockUntil: account.lockUntil
            });

            return { success: true, uid: userCredential.user.uid }
        } catch (error) {
            return {success: false, error: error.message}
        }
    },

    resetPassword: async function (email) {
        try {
            const auth = firebase.auth();
            await auth.sendPasswordResetEmail(email);
            return { success: true };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },
    
    login: async function (email,password) {
        try {
            const auth = firebase.auth();
            const userCredential = await auth.signInWithEmailAndPassword(email, password);
            const idToken = await userCredential.user.getIdToken(true);
            return { success: true, uid: userCredential.user.uid, idToken: idToken }
        } catch (error) {
            return { success: false, errorCode: error.code, error: error.message}
        }
    },

    logout: async function () {
        const auth = firebase.auth();
        await auth.signOut();
    },

    // Listen for auth state changes (optional)
    onAuthStateChanged: function (dotNetRef) {
        firebase.auth().onAuthStateChanged(user => {
            dotNetRef.invokeMethodAsync('OnAuthStateChanged', user ? user.uid : null);
        });
    },

    // Real-time listener for messages in a chat room
    listenToMessages: function (chatRoomId, dotNetRef) {
        // Unsubscribe previous listener if any
        if (!window._messageListeners) window._messageListeners = {};
        if (window._messageListeners[chatRoomId]) {
            window._messageListeners[chatRoomId]();
        }

        const unsubscribe = firebase.firestore()
            .collection('Notifications')
            .doc(chatRoomId)
            .collection('messages')
            .orderBy('createdAt')
            .onSnapshot(snapshot => {
                const messages = [];
                snapshot.forEach(doc => {
                    const data = doc.data();
                    messages.push({
                        id: doc.id,
                        chatRoomId: data.chatRoomId,
                        senderId: data.senderId,
                        receiverRole: data.receiverRole,
                        category: data.category,
                        message: data.message,
                        isRead: data.isRead,
                        createdAt: data.createdAt,
                    });
                });

                dotNetRef.invokeMethodAsync('ReceiveMessages', messages);
            });

        window._messageListeners[chatRoomId] = unsubscribe;
    },

    // Add a message to Firestore
    sendMessage: async function (chatRoomId, notif) {
        try {
            if (!notif.senderId) {
                console.error("Cannot send message: userId is null");
                return {success: false, error: "User id is null"};
            }
            await firebase.firestore()
                .collection('Notifications')
                .doc(chatRoomId)
                .collection('messages')
                .add({
                    chatRoomId: notif.chatRoomId,
                    senderId: notif.senderId,
                    receiverRole: notif.receiverRole,
                    category: notif.category,
                    message: notif.message,
                    isRead: notif.isRead,
                    createdAt: notif.createdAt,
                });
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
        
    },
}