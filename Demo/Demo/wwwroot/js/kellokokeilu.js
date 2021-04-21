setInterval(AsetaKello, 1000);

const tunnit = document.getElementById("tunnit");
const minuutit = document.getElementById("minuutit");
const sekunnit = document.getElementById("sekunnit");

function AsetaKello() {
    const aika = new Date();
    const sekuntia = aika.getSeconds() / 60;
    const minuuttia = (aika.getMinutes() + sekuntia) / 60
    const tuntia = (aika.getHours() + minuuttia) / 12
    AsetaViisaria(sekuntia, sekunnit);
    AsetaViisaria(minuuttia, minuutit);
    AsetaViisaria(tuntia, tunnit);
}

function AsetaViisaria(paljonko, elementti) {
    elementti.style.setProperty('--kierto', paljonko * 360);
}

AsetaKello();