window.firestoreFunctions = {
    addMember: async function (member) {
        const db = firebase.firestore();
        await db.collection("Members").add(member);
    },

    getDepartments: async function () {
        const db = firebase.firestore();
        const snapshot = await db.collection("Departments").get();
        return snapshot.docs.map(doc => ({ id: doc.id, ...doc.data() }));
    },

    addDepartment: async function (department) {
        const db = firebase.firestore();
        await db.collection("Departments").add(department);
    },

    addUser: async function (user) {
        const db = firebase.firestore();
        await db.collection("Accounts").add(user);
        alert("Account Successfuly Added!");
    }
};