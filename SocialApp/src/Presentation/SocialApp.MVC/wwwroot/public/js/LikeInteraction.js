let lastLikeState = null;

$(document).on("click", ".like-button", function (e) {
    e.preventDefault();

    const button = $(this);
    const userId = button.attr("data-user-id");
    const postId = button.attr("data-post-id");
    const isCurrentlyLiked = button.attr("data-is-liked") === "True";
    const likeCountElement = button.closest("span").find(".like-count");
    let currentCount = parseInt(likeCountElement.text());

    // UI Güncelle
    if (isCurrentlyLiked) {
        button.attr("data-is-liked", "False");
        button.html('<i class="bi bi-heart text-danger fs-3"></i>');
        likeCountElement.text(Math.max(0, currentCount - 1));
        lastLikeState = { userId, postId, isLike: false };
    } else {
        button.attr("data-is-liked", "True");
        button.html('<i class="bi bi-heart-fill text-danger fs-3"></i>');
        likeCountElement.text(currentCount + 1);
        lastLikeState = { userId, postId, isLike: true };
    }
});

// Sayfa kapanırken sadece son like durumu gönderilir
window.addEventListener("beforeunload", function () {
    if (lastLikeState !== null) {
        navigator.sendBeacon(
            "/Post/UpdatePostLike",
            new Blob(
                [JSON.stringify(lastLikeState)],
                { type: "application/json" }
            )
        );
    }
});