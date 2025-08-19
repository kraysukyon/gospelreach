window.firestoreFunctions = {

    //============================================User Section============================================//

    //Get Accounts
    async getAccounts() {
        try {
            const snapshot = await db.collection("Users").get();
            return snapshot.docs.map(doc => ({ id: doc.id, ...doc.data() }));
        } catch (error) {
            alert(error)
        }
    },
    //Update Account
    async updateAccount(docId, newRole) {
        try {
            await db.collection("Users").doc(docId).update({
                role: newRole
            });
        } catch (error) {
            alert(error)
        }
    },

    //Disable Account
    async disableAccount(docId) {
        try {
            await db.collection("Users").doc(docId).update({ status: "Disabled" });
        } catch (error) {
            alert(error)
        }
    },
    //Enable Account
    async enableAccount(docId) {
        try {
            await db.collection("Users").doc(docId).update({ status: "Active" });
        } catch (error) {
            alert(error)
        }
    },

    //============================================Attendance Section============================================//

    //Getting Attendance
    async getAttendance() {
        try {
            const attendanceTable = await db.collection("Attendance").get();
            const attendance = attendanceTable.docs.map(items => ({ id: items.id, ...items.data() }));
            return { success: true, data: attendance };
        } catch (error) {
            return { success: false, error: error.message }
        }
        
    },

    //Adding Attendance
    async addAttendance(attendance) {
        try {
            await db.collection("Attendance").add({
                scheduleId: attendance.scheduleId,
                date: attendance.date,
                count: attendance.count,
                seekers: attendance.seekers
            });
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //update attendance
    async editAttendance(docId, attendance) {
        try {
            await db.collection("Attendance").doc(docId).update({
                scheduleId: attendance.scheduleId,
                date: attendance.date,
                count: attendance.count,
                seekers: attendance.seekers
            });
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message } 
        }
    },

    //delete attendance
    async deleteAttendance(docId) {
        try {
            await db.collection("Attendance").doc(docId).delete();
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },


    //============================================Member Management Section============================================//
    //fetch member list
    async getMembers() {
        try {
            const membersTable = await db.collection("Members").get();
            const member = membersTable.docs.map(items => ({ id: items.id, ...items.data() }));

            return { success: true , data: member }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //add member
    async addMember(member) {
        try {
            await db.collection("Members").add({
                firstName: member.firstName,
                middleName: member.middleName,
                lastName: member.lastName,
                gender: member.gender,
                email: member.email,
                contact: member.contact,
                birthdate: member.birthdate,
                age: member.age,
                classification: member.classification,
                status: member.status,
            });
            return { success: true }
        } catch (error) {
            return { success: false }
            alert(error)
        }
    },

    //update memeber
    async updateMember(docId, member) {
        try {
            await db.collection("Members").doc(docId).set(member);
        } catch (error) {
            alert(error)
        }
    },

    //remove member
    async deleteMember(docId) {
        try {
            await db.collection("Members").doc(docId).delete();
        } catch (error) {
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

    //============================================Music Management Section============================================//
    async getSongs() {
        try {
            const songTable = await db.collection("Songs").get();
            return songTable.docs.map(items => ({ id: items.id, ...items.data() }));
        } catch (error) {
            alert(error)
        }
    },

    async addSong(song) {
        try {
            await db.collection("Songs").add({
                title: song.title,
                artist: song.artist,
                lyricsAndChords: song.lyricsAndChords
            });

            return { success: true };
        } catch (error) {
            return { success: false };
            alert(error);
        }
    },

    async updateSong(docId, song) {
        try {
            await db.collection("Songs").doc(docId).update({
                title: song.title,
                artist: song.artist,
                lyricsAndChords: song.lyricsAndChords
            });
        } catch (error) {
            alert(error)
        }
    },

    async deleteSong(id) {
        try {
            await db.collection("Songs").doc(id).delete();
        } catch (error) {
            alert(error);
        }
    },

    resizeTextarea(input, output) {
        input.style.height = 'auto';
        if (!input || input.value.trim() === '') {
            //input.style.height = "100px";
            //output.style.height = "100px";
            input.style.height = (input.scrollHeight) + "px";
        }
        else {
            input.style.height = (input.scrollHeight) + "px";
            output.style.height = input.style.height;
        }
        
    },

    //============================================Department Management Section============================================//
    async getDepartment() {
        try {
            const departmentTable = await db.collection("Departments").get();
            const departments = departmentTable.docs.map(items => ({ departmentId: items.id, ...items.data() }));

            return { success: true, data: departments };
        } catch (error) {
            return { success: false, error: error.message};
        }
    },

    async addDepartment(department) {
        try {
            await db.collection("Departments").add({
                departmentName: department.departmentName,
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async getDepartmentById(id) {
        try {
            const docRef = db.collection("Departments").doc(id);
            const doc = await docRef.get();

            if (doc.exists) {
                return { success: true, data: { id: doc.id, ...doc.data() } };
            }

            return { success: false, error: "Department not found" };
        } catch (error) {
            return { success: false, error: error.message}
        }
    },

    async updateDepartment(id,department) {
        try {
            await db.collection("Departments").doc(id).update({
                departmentName: department.departmentName
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message}
        }
    },

    async removeDepartment(id) {
        try {
            await db.collection("Departments").doc(id).delete();

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    //============================================DepartmentMember Management Section============================================//

    async getDepartmentMembers() {
        try {
            //const membersTable = await db.collection("DepartmentMembers").where("departmentId", "==", depId).get();
            const membersTable = await db.collection("DepartmentMembers").get();
            const members = membersTable.docs.map(item => ({ DepartmentMemberId: item.id, ...item.data() }));

            return { success: true, data: members };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },

    async addDepartmentMember(member) {
        try {
            await db.collection("DepartmentMembers").add({
                departmentId: member.departmentId,
                memberId: member.memberId
            });

            return { success: true };
        } catch (error) {
            return { success: false, error: error.message };
        }
    },
    
    async updateDepartmmentMember(id, member) {
        try {
            await db.collection("DepartmentMembers").doc(id).set(member);
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async removeDepartmentmember(id) {
        try {
            await db.collection("DepartmentMembers").doc(id).delete();
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    //============================================DepartmentMember Management Section============================================//

    async addSchedule(schedule) {
        try {
            await db.collection("Schedules").add({
                category: schedule.category,
                title: schedule.title,
                startDate: schedule.startDate,
                endDate: schedule.endDate,
                startTime: schedule.startTime,
                endTime: schedule.endTime,
                timeOption: schedule.timeOption,
                location: schedule.location,
                description: schedule.description,
                status: schedule.status
            });

            return { success: true }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async getSchedule() {
        try {
            const schedTable = await db.collection("Schedules").get();
            const sched = schedTable.docs.map(u => ({id: u.id, ... u.data()}));

            return { success: true, data: sched }
        } catch (error) {
            return { success: false, error: error.message}
        }
    },

    async getScheduleByStatus(stats) {
        try {
            const schedTable = await db.collection("Schedules").where("status", "==", stats).get();
            const sched = schedTable.docs.map(u => ({ id: u.id, ...u.data() }));

            return {success: true, data: sched }
        } catch (error) {
            return { success: false, error: error.message }
        }
    },

    async updateSchedule(id, schedule) {
        try {
            await db.collection("Schedules").doc(id).update({
                category: schedule.category,
                title: schedule.title,
                startDate: schedule.startDate,
                endDate: schedule.endDate,
                startTime: schedule.startTime,
                endTime: schedule.endTime,
                timeOption: schedule.timeOption,
                location: schedule.location,
                description: schedule.description,
                status: schedule.status
            });
            return { success: true }
        } catch (error) {
            return { success: true, error: error.message }
        }
    },

    async removeSchedule(Id) {
        try {
            await db.collection("Schedules").doc(Id).delete();
            return { success: true }
        } catch (error) {
            return { success: false, error: error.message}
        }
    },

    textareaResize(input) {
        input.style.height = 'auto';
        //input.style.height = this.scrollHeight + 'px';

        if (!input || input.value.trim() === '') {
            //input.style.height = 'calc(1.5em + .75rem + 2px)';
            input.style.height = (input.scrollHeight) + "px";
        }
        else {
            input.style.height = (input.scrollHeight) + "px";
        }
    },

}