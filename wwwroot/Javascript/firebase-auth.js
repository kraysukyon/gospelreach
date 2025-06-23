const auth = firebase.auth();

window.firebaseAuth = {
    register: async function (email, password, user) {
        try {
            const userCredential = await auth.createUserWithEmailAndPassword(email, password);
            const uid = userCredential.user.uid;

            const db = firebase.firestore();
            //await db.collection("Accounts").add(user);
            await db.collection("Accounts").doc(uid).set({
                email: user.email || "",
                firstName: user.firstName || "",
                lastName: user.lastName || "",
                contact: user.contact || "",
                role: user.role || "",
                status: user.stats || "Active",
                dateOfCreation: user.dateOfCreation || "",
            });

            return { success: true, uid: userCredential.user.uid }
        } catch (error) {
            return {success: false, error: error.message}
        }
    },

    login: async function (email,password) {
        try {
            const userCredential = await auth.signInWithEmailAndPassword(email, password);
            return { success: true, uid: userCredential.user.uid }
        } catch (error) {
            return { success: false, error: error.message}
        }
    },

    logout: async function () {
        await auth.signOut();
    }
}

//const auth = firebase.auth();

//window.firebaseAuth = {
//    register: async function (email, password) {
//        try {
//            const userCredential = await auth.createUserWithEmailAndPassword(email, password);
//            return { success: true, uid: userCredential.user.uid };
//        } catch (error) {
//            return { success: false, error: error.message };
//        }
//    },

//    login: async function (email, password) {
//        try {
//            const userCredential = await auth.signInWithEmailAndPassword(email, password);
//            return { success: true, uid: userCredential.user.uid };
//        } catch (error) {
//            return { success: false, error: error.message };
//        }
//    },

//    logout: async function () {
//        await auth.signOut();
//    }
//};