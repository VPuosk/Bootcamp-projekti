const userAction = async () => {
    const vastaus = await fetch('https://localhost:44358/TietokantaAPI/Reseptit');
    const json = await vastaus.json();
    var paikka = document.getElementById("listanpaikka");
    /*
    
    ul = document.createElement('ul');
    paikka.appendChild(ul);
    for (var i = 0; i < json.length; i++) {
        let li = document.createElement('li');
        ul.appendChild(li);
        li.id += "Resepti" + json[i].id;
        li.innerHTML += json[i].name;
        console.log(json[i].id)
    }
    */

    // eli tehdään tästä Bootstrap tyyppinen taulukko lennossa
    // lähinnä JS ja muuta kokeilua.
    // eli pystynkö kokoamaan sivun tätäkin kautta suoraan API:n kautta ilman MVC puolta.
    let ryhmittely = document.createElement('div');
    ryhmittely.classList.add('container','border','border-primary');
    paikka.appendChild(ryhmittely);
    for (var i = 0; i < json.length; i++) {
        let rivi = document.createElement('div');
        rivi.classList.add('row');
        ryhmittely.appendChild(rivi);
        let sarake = document.createElement('div');
        sarake.classList.add('col-sm-9');
        rivi.appendChild(sarake);
        let linkki = document.createElement('a');
        sarake.appendChild(linkki);
        linkki.id += "Resepti" + json[i].id;
        console.log(window.location.hostname);
        console.log(window.location.port);
        console.log(linkki.href);
        linkki.href += "https://" + window.location.hostname + ":" + window.location.port + "/Tietokanta/Resepti/" + json[i].id;
        linkki.innerHTML += json[i].nimi;
        //console.log(json[i].id)
        let aikasarake = document.createElement('div');
        aikasarake.classList.add('col-sm-3');
        rivi.appendChild(aikasarake);
        aikasarake.innerHTML = json[i].luotu;
    }
}

console.log("polku" + window.location.pathname);

function testi() {
    var palat = window.location.pathname.split('/');
    console.log(palat[palat.length-1]);
}
testi();
userAction();