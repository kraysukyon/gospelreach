window.firestoreFunctions = {

    //============================================Account Section============================================//

    //Get Accounts
    async getAccounts() {
        try {
            const snapshot = await db.collection("Accounts").get();
            return snapshot.docs.map(doc => ({ id: doc.id, ...doc.data() }));
        } catch (error) {
            alert(error)
        }
    },
    //Update Account
    async updateAccount(docId, account) {
        try {
            await db.collection("Accounts").doc(docId).set(account);
        } catch (error) {
            alert(error)
        }
    },

    //Disable Account
    async disableAccount(docId) {
        try {
            await db.collection("Accounts").doc(docId).update({ stats: "Disabled" });
        } catch (error) {
            alert(error)
        }
    },
    //Enable Account
    async enableAccount(docId) {
        try {
            await db.collection("Accounts").doc(docId).update({ stats: "Active" });
        } catch (error) {
            alert(error)
        }
    },

    //============================================Attendance Section============================================//

    //Getting Attendance
    async getAttendance() {
        try {
            const attendanceTable = await db.collection("Attendance").get();
            return attendanceTable.docs.map(items => ({ id: items.id, ...items.data() }));
        } catch (error) {
            alert(error);
        }
        
    },

    //Adding Attendance
    async addAttendance(attendance) {
        try {
            const attendanceToAdd = await db.collection("Attendance").add({
                date: attendance.date,
                service: attendance.service,
                count: attendance.count,
                seekers: attendance.seekers
            });
            return { success: true, id: attendanceToAdd.id }
        } catch (error) {
            return { success: false, error: error.message}
        }
        
    },

    async editAttendance(docId, attendance) {
        try {
            await db.collection("Attendance").doc(docId).set(attendance);
        } catch (error) {
            alert(error)
        }
    }
}