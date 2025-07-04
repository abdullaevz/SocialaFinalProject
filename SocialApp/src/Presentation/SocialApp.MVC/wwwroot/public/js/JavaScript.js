document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.comment-trigger').forEach(trigger => {
        trigger.addEventListener('click', function (e) {
            e.preventDefault();

            const commentSection = this.closest('.card').querySelector('.comments-section');

            if (commentSection.style.display === 'none' || commentSection.style.display === '') {
                commentSection.style.display = 'block';
                this.querySelector('span').textContent = 'Close Comments';
            } else {
                commentSection.style.display = 'none';
                this.querySelector('span').textContent = 'Comments';
            }
        });
    });
});