/* helper method to handle failures in SignalR connections */
function handleSignalRFail(data) {
    $.jGrowl(data);
    console.log(data);
}
