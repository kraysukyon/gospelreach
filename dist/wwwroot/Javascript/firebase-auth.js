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
    }
}