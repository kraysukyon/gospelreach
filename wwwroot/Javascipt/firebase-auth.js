const auth = firebase.auth();

window.firebaseAuth = {
    register: async function (email, password) {
        try {
            const userCredential = await auth.createUserWithEmailAndPassword(email, password);
            return { success: true, uid: userCredential.user.uid };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    login: async function (email, password) {
        try {
            const userCredential = await auth.signInWithEmailAndPassword(email, password);
            return { success: true, uid: userCredential.user.uid };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    logout: async function () {
        await auth.signOut();
    }
};