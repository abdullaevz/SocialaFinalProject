let selectedUserId = null;
let connection = null;

// Kullanıcı tıklandığında sohbet başlatılır
function openChat(userName, initials, userId) {
    selectedUserId = userId;

    // Aktif kullanıcı görselini güncelle
    document.querySelectorAll('.user-item').forEach(item => {
        item.classList.remove('active');
    });

    event.currentTarget.classList.add('active');

    // Header kısmını güncelle
    document.querySelector('.chat-header h4').textContent = userName;
    document.querySelector('.chat-header .user-avatar').textContent = initials;

    // Mesajları temizle
    const chatMessages = document.getElementById('chatMessages');
    chatMessages.innerHTML = '';

    // Örnek mesajları göster (test amaçlı)
    // Gerçekte sunucudan AJAX/SignalR ile getirilecek
    if (userName === 'Ahmet Yılmaz') {
        addMessage('received', 'Merhaba, nasılsın?', '10:00 AM');
        addMessage('sent', 'İyiyim, teşekkür ederim. Sen nasılsın?', '10:02 AM');
        addMessage('received', 'Bugünkü proje toplantısına katılacak mısın?', '10:03 AM');
    }

    chatMessages.scrollTop = chatMessages.scrollHeight;

    // Mobil görünüm için alanları değiştir
    if (window.innerWidth <= 768) {
        document.getElementById('userList').classList.remove('active');
        document.getElementById('chatArea').classList.add('active');
    }
}

// Yeni mesaj DOM'a eklenir
function addMessage(type, text, time) {
    const chatMessages = document.getElementById('chatMessages');
    const messageDiv = document.createElement('div');
    messageDiv.className = `message ${type}`;
    messageDiv.innerHTML = `
        <div class="message-content">${text}</div>
        <div class="message-time">${time}</div>
    `;
    chatMessages.appendChild(messageDiv);
    chatMessages.scrollTop = chatMessages.scrollHeight;
}

// Mesaj gönderme fonksiyonu
function sendMessage() {
    const input = document.getElementById('messageInput');
    const messageText = input.value.trim();

    if (messageText !== '') {
        const now = new Date();
        const timeString = now.getHours() + ':' + (now.getMinutes() < 10 ? '0' : '') + now.getMinutes();

        // Mesajı kendi ekranına ekle
        addMessage('sent', messageText, timeString);

        // SignalR ile karşı tarafa gönder
        connection.invoke("SendMessage", currentUserName, selectedUserId, messageText)
            .catch(err => console.error(err.toString()));

        input.value = '';
    }
}

// SignalR bağlantısını başlat
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

    connection.on("receiveMessage", function (fromUser, message) {
        const now = new Date();
        const timeString = now.getHours() + ':' + (now.getMinutes() < 10 ? '0' : '') + now.getMinutes();
        addMessage('received', message, timeString);
    });

    connection.start()
        .then(() => console.log("SignalR bağlantısı kuruldu."))
        .catch(err => console.error("SignalR bağlantısı hatası:", err));
}

// Sayfa yüklendiğinde çalışacak kod
window.onload = function () {
    setupSignalR();

    // Enter tuşuna basıldığında mesaj gönder
    document.getElementById('messageInput').addEventListener('keypress', function (e) {
        if (e.key === 'Enter') {
            sendMessage();
        }
    });

    // Mobil görünüm için kullanıcı listesi varsayılan olarak açık
    if (window.innerWidth <= 768) {
        document.getElementById('userList').classList.add('active');
        document.getElementById('chatArea').classList.remove('active');
    }
};
