//const auth = firebase.auth();

window.firebaseAuth = {
    register: async function (email, password, account) {
        try {
            const auth = firebase.auth();
            const userCredential = await auth.createUserWithEmailAndPassword(email, password);
            const uid = userCredential.user.uid;

            const db = firebase.firestore();
            //await db.collection("Accounts").add(user);
            await db.collection("Accounts").doc(uid).set({
                memberId: account.memberId,
                role: account.role,
                status: account.status,
            });

            return { success: true, uid: userCredential.user.uid }
        } catch (error) {
            return {success: false, error: error.message}
        }
    },

    
    login: async function (email,password) {
        try {
            const auth = firebase.auth();
            const userCredential = await auth.signInWithEmailAndPassword(email, password);
            const idToken = await userCredential.user.getIdToken(true);
            return { success: true, uid: userCredential.user.uid, idToken: idToken }
        } catch (error) {
            return { success: false, error: error.message}
        }
    },

    logout: async function () {
        const auth = firebase.auth();
        await auth.signOut();
    }
}