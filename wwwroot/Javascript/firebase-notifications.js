export async function requestNotificationPermission() {
    try {
        const token = await messaging.getToken({ vapidKey: "BNlOKIO-yHAS3KByumFSIu1YRw3xyyAIbt5BcbpEX_4qXbb222P3qn8kHwX4BxhbR6KruylJwDhLhWlM3dP9qTU" });
        return token; // Send to backend
    } catch (err) {
        console.error("Unable to get permission for notifications", err);
        return null;
    }
}