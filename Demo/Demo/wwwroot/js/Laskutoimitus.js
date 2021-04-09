// muuttuja jolla valvotaan desimaalipisteen käyttöä varsinaisen syöttökentän yhteydessä
var onkoPistettaKaytetty = false;

// varsinainen laskentafunktio
// hakee merkityistä kentistä (tulosruutu, vanhatulosruutu) muuttujat
// käyttää operaattoriksi valittua toimintoa laskentaan
function LaskeLasku() {
    let vanhaArvo = document.getElementById("vanhatulosruutu").innerHTML;
    let uusiArvo = document.getElementById("tulosruutu").innerHTML;
    let operaattori = document.getElementById("operaattori").innerHTML;
    if ((vanhaArvo != "&nbsp;") && (uusiArvo != "&nbsp;") && (operaattori != "&nbsp;")) {
        let uusiLuku = Number(uusiArvo);
        let vanhaLuku = Number(vanhaArvo);

        switch (operaattori) {
            case "+":
                vanhaLuku += uusiLuku;
                break;
            case "-":
                vanhaLuku -= uusiLuku;
                break;
            case "*":
                vanhaLuku *= uusiLuku;
                break;
            case "/":
                vanhaLuku /= uusiLuku;
                break;
            case "%":
                vanhaLuku %= uusiLuku;
                break;
            default:
                return;
        }
        document.getElementById("vanhatulosruutu").innerHTML = vanhaLuku;
        document.getElementById("tulosruutu").innerHTML = "&nbsp;";
        onkoPistettaKaytetty = false;
    }
}

// funktio uusien numeroiden lisäämikseksi merkkijonomuotoiseen kenttään
function LisaaLukuun() {
    let uusiArvo = document.getElementById("tulosruutu").innerHTML;
    if (uusiArvo == "&nbsp;") {
        uusiArvo = "";
    }
    document.getElementById("tulosruutu").innerHTML = uusiArvo + document.activeElement.innerHTML;
}

// erillinen funktio mahdollisen desimaalipisteen käyttämiselle.
function PisteenLisays() {
    // tarkistetaan onko desimaalipiste jo asetettu, jos on, poistutaan
    if (onkoPistettaKaytetty) {
        return;
    }

    // lisätään desimaalipiste
    let uusiArvo = document.getElementById("tulosruutu").innerHTML;
    onkoPistettaKaytetty = true;
    if (uusiArvo == "&nbsp;") {
        uusiArvo = "0";
    }
    document.getElementById("tulosruutu").innerHTML = uusiArvo + document.activeElement.innerHTML;
}

// funktio laskentatilanteen nollaamiseksi
function NollaaLaskuri() {
    document.getElementById("tulosruutu").innerHTML = "&nbsp;";
    document.getElementById("vanhatulosruutu").innerHTML = "&nbsp;";
    document.getElementById("operaattori").innerHTML = "&nbsp;";
    onkoPistettaKaytetty = false;
}

// funktio, jolla voidaan vaihtaa käytössä olevaa laskentaoperaattoria
// sama funktio kutsuu laskenta funktiota jos tarvittavat kentät on valmiiksi täytetty.
function VaihdaOperaattoria() {
    document.getElementById("operaattori").innerHTML = document.activeElement.innerHTML;
    let nykyinenArvo = document.getElementById("tulosruutu").innerHTML;
    let vanhaArvo = document.getElementById("vanhatulosruutu").innerHTML;

    // eli aiempi arvo kentässä ei ole arvoa, siirretään uusi arvo vanhaan kenttään ja tyhjennetään uusi kenttä, ei kutsuta lasku funktiota
    // muutoin tehdään laskutoimitus
    if (vanhaArvo == "&nbsp;") {
        //console.warn("vaihtamassa operaattoria - VANHA KENTTÄ Tyhjä!")
        document.getElementById("vanhatulosruutu").innerHTML = nykyinenArvo;
        document.getElementById("tulosruutu").innerHTML = "&nbsp;";
        onkoPistettaKaytetty = false;
    } else {
        //console.warn("vaihtamassa operaattoria - VANHA KENTTÄ Täytetty!")
        LaskeLasku();
    }
}

// funktio, jolla voidaan vaihtaa aktiivisen kentän arvon etumerkkiä.
function MerkinVaihto() {
    let nykyinenArvo = document.getElementById("tulosruutu").innerHTML;

    //ei vaihdeta merkkiä, jos kenttä on tyhjä
    if (nykyinenArvo == "&nbsp;") {
        return;
    }

    // tarkastetaan ensimmäisen merkin arvo
    if (nykyinenArvo.slice(0, 1) == "-") {
        // kentän arvolla oli jo '-' merkki, poistetaan se.
        document.getElementById("tulosruutu").innerHTML = nykyinenArvo.slice(1);
    } else {
        // kentän arvolla ei ollut '-' merkkiä, joten lisätään sellainen
        document.getElementById("tulosruutu").innerHTML = "-" + nykyinenArvo;
    }
}

// mahdollisuus poistaa virhepainallus (merkkijonomuotoisen muuttujan viimeisen arvon poisto
function PoistaLuku() {
    let nykyinenArvo = document.getElementById("tulosruutu").innerHTML;

    // mikäli kentässä oli arvoja, niin poistetaan niistä viimeinen
    if (nykyinenArvo != "") {

        // mikäli viimeinen arvo oli piste. vapautetaan desimaalimuuttuja.
        if (nykyinenArvo.slice(-1) == ".") {
            onkoPistettaKaytetty = false;
        }

        // palautetaan kaikki paitsi viimeinen merkki
        nykyinenArvo = nykyinenArvo.slice(0, -1)
    }

    //varmistetaan, ettei kenttä jää tyhjäksi
    if (nykyinenArvo == "") {
        nykyinenArvo = "&nbsp;";
    }

    // korvataan arvo halutulla merkkijonolla
    document.getElementById("tulosruutu").innerHTML = nykyinenArvo;
}
