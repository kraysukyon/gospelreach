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
            await db.collection("Attendance").add({
                date: attendance.date,
                service: attendance.service,
                count: attendance.count,
                seekers: attendance.seekers
            });
            return { success: true }
        } catch (error) {
            return { success: false }
        }
    },

    async editAttendance(docId, attendance) {
        try {
            await db.collection("Attendance").doc(docId).set(attendance);
        } catch (error) {
            alert(error)
        }
    },

    async deleteAttendance(docId) {
        try {
            await db.collection("Attendance").doc(docId).delete();
        } catch (error) {
            alert(error)
        }
    },


    //============================================Member Management Section============================================//
    async getMembers() {
        try {
            const membersTable = await db.collection("Members").get();
            return membersTable.docs.map(items => ({id: items.id, ...items.data()}));
        } catch (error) {
            alert(error)
        }
    },

    async addMember(member) {
        try {
            await db.collection("Members").add({
                firstName: member.firstName,
                middleName: member.middleName,
                lastName: member.lastName,
                email: member.email,
                contact: member.contact,
                birthdate: member.birthdate,
                dateOfSoldiership: member.dateOfSoldiership,
                classification: member.classification,
                status: member.status,
            });
            return { success: true }
        } catch (error) {
            return { success: false }
            alert(error)
        }
    },

    //============================================Event Management Section============================================//
    async getEvents() {
        try {
            const eventsTable = await db.collection("Events").get();
            return eventsTable.docs.map(items => ({ id: items.id, ...items.data() }));
        } catch (error) {
            alert(error)
        }
    },

    async addEvent(event) {
        try {
            await db.collection("Events").add({
                eventName: event.eventName,
                date: event.date,
                tag: event.tag,
                startTime: event.startTime,
                endTime: event.endTime,
                location: event.location,
                description: event.description
            });
            return { success: true }
        } catch (error) {
            return { success: false }
            alert(error)
        }
    },

    async editEvent(eventId, events) {
        try {
            await db.collection("Events").doc(eventId).set(events);
        } catch (error) {
            alert(error)
        }
    },

    async deleteEvent(eventId) {
        try {
            await db.collection("Events").doc(eventId).delete();
        } catch (error) {
            alert(error);
        }
    },
    
}