window.firestoreFunctions = {
    async getAccounts() {
        try {
            const snapshot = await db.collection("Accounts").get();
            return snapshot.docs.map(doc => ({ id: doc.id, ...doc.data() }));
        } catch (error) {
            alert(error)
        }
    },

    async updateAccount(docId, account) {
        try {
            await db.collection("Accounts").doc(docId).set(account);
        } catch (error) {
            alert(error)
        }
    },

    async disableAccount(docId) {
        try {
            await db.collection("Accounts").doc(docId).update({ stats: "Disabled" });
        } catch (error) {
            alert(error)
        }
    },

    async enableAccount(docId) {
        try {
            await db.collection("Accounts").doc(docId).update({ stats: "Active" });
        } catch (error) {
            alert(error)
        }
    }
}