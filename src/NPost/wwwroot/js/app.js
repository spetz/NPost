'use strict';
(function() {
    const $message = document.getElementById('message');
    const $sendMessage = document.getElementById('send-message');
    const $messages = document.getElementById('messages');
    const connection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:5000/npost')
        .configureLogging(signalR.LogLevel.Information)
        .build();

    appendMessage('Connecting to NPost Hub...');
    connection.start()
        .then(() => {
        appendMessage('Connected.', 'primary');
    })
    .catch(err => appendMessage(err, 'danger'));
    
    $sendMessage.onclick = function() {
        const message = $message.value;
        if (message.match(/^\s*$/)) {
            alert('Empty message.');
            return;
        }
        appendMessage(`Sent a message: ${message}`);
        $message.value = '';
        connection.invoke('sendMessage', message)
        .then(response => {
            if (response.startsWith("Invalid")) {
                appendMessage(response, 'danger');
                return;
            }
            appendMessage(response);
        })
        .catch(err => appendMessage(err, 'danger'));;
    };
    
    function appendMessage(message, type, data) {
        var dataInfo = '';
        if (data) {
            dataInfo += '<div>' + JSON.stringify(data) + '</div>';
        }
        $messages.innerHTML += `<li class='list-group-item list-group-item-${type}'>${message} ${dataInfo}</li>`;
    }
})();