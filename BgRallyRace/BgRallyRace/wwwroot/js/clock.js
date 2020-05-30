function clock(input) {
    "use strict";
    var date = input;   //Type "April 25, 2020 19:00:00";
    var countDownDate = new Date(date).getTime();

    var x = setInterval(function () {
        var now = new Date().getTime();

        var distance = countDownDate - now;

        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        document.getElementById("demo").innerHTML = days + "дни " + hours + "час. "
            + minutes + "мин. " + seconds + "сек. ";

        if (distance < 0) {
            clearInterval(x);
            document.getElementById("demo").innerHTML = "Състезанието започна";
        }
    });
}
