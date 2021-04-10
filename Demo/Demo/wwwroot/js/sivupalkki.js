// TARKOITETTU SIVUNAVIGOINNIN TOIMINTAAN
// lähde: W3School
/* Set the width of the side navigation to 250px and the left margin of the page content to 250px */
function avaaSivupalkki() {
    document.getElementById("sivuPalkki").style.width = "250px";
    document.getElementById("main").style.marginLeft = "250px";
}

/* Set the width of the side navigation to 0 and the left margin of the page content to 0 */
function suljeSivupalkki() {
    document.getElementById("sivuPalkki").style.width = "0";
    document.getElementById("main").style.marginLeft = "0";
}