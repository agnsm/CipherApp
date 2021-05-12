document.addEventListener('DOMContentLoaded', function () {
    const buttonEnc = document.querySelector('.encoder .button-primary');
    const encoder = document.querySelector('#encoder');
    const positionEnc = encoder.getBoundingClientRect().top;
    buttonEnc.addEventListener('click', event => {
        sessionStorage.setItem("scroll-position", positionEnc);
        sessionStorage.removeItem("clear-data");
    });

    const buttonDec = document.querySelector('.decoder .button-primary');
    const decoder = document.querySelector('#decoder');
    const positionDec = decoder.getBoundingClientRect().top;
    buttonDec.addEventListener('click', event => {
        sessionStorage.setItem("scroll-position", positionDec);
        sessionStorage.removeItem("clear-data");
    });


    if (sessionStorage.getItem("scroll-position") != null) {
        window.scrollTo(0, sessionStorage.getItem("scroll-position"));
        sessionStorage.removeItem("scroll-position");
    }

    if (sessionStorage.getItem("clear-data") != null) {
        document.querySelectorAll("input[type='text']").forEach((x) => {
            x.value = "";
        });
        document.querySelectorAll("textarea").forEach((x) => {
            x.value = "";
        });
    }

    sessionStorage.setItem("clear-data", "true");
});
