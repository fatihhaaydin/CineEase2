document.addEventListener('DOMContentLoaded', function () {
    const clickablePhotos = document.querySelectorAll('.clickable-photo');
    clickablePhotos.forEach(photo => {
        photo.addEventListener('click', function () {
            const photoId = this.getAttribute('data-id');
            window.location.href = `/Home/Details?photo=${photoId}`;
        });
    });
});