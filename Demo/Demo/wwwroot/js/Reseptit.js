const userAction = async () => {
    const vastaus = await fetch('/TietokantaAPI/Reseptit');
    const json = await vastaus.json();
    var paikka = document.getElementById("listanpaikka");

    // eli tehdään tästä Bootstrap tyyppinen taulukko lennossa
    // lähinnä JS ja muuta kokeilua.
    // eli pystynkö kokoamaan sivun tätäkin kautta suoraan API:n kautta ilman MVC puolta.
    let ryhmittely = document.createElement('div');
    ryhmittely.classList.add('container', 'border', 'border-primary');
    ryhmittely.style = 'background-color: rgba(240, 240, 255, 0.95);';
    paikka.appendChild(ryhmittely);
    for (var i = 0; i < json.length; i++) {

        // luodaan rivielementti (Boostrap) ja liitetään se ryhmittely elementtiin
        let rivi = document.createElement('div');
        rivi.classList.add('row', 'm-2', 'p-2');
        rivi.style = 'background-color: rgba(100, 100, 255, 0.2);';
        ryhmittely.appendChild(rivi);

        // luodaan ensimmäinen sarake elementti ja liitetään se rivi elementtiin
        let sarake = document.createElement('div');
        sarake.classList.add('col-sm-9');
        rivi.appendChild(sarake);

        // täytetään ensimmäiseen sarakkeeseen menevä linkkitieto ja elementin id tieto (josko tarpeen)
        // täytetään myös varsinainen innerHTML kenttä.
        let linkki = document.createElement('a');
        sarake.appendChild(linkki);
        linkki.id += "Resepti" + json[i].id;
        linkki.href += "/Tietokanta/Resepti/" + json[i].id;
        linkki.innerHTML += json[i].nimi;
        //console.log(json[i].id)

        // luodaan toinen sarake elementti ja liitetään se rivi elementtiin
        let aikasarake = document.createElement('div');
        aikasarake.classList.add('col-sm-3');
        rivi.appendChild(aikasarake);
        var aika = new Date(json[i].luotu);
        // puretaan aika ja kasataan uudelleen (haluttuun muotoon)
        aikasarake.innerHTML = aika.toLocaleDateString() + " " + aika.getHours() + "." + aika.getMinutes();

        //2021-04-21T07:26:00
    }
}

userAction();