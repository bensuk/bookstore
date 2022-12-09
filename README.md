# API specifikacija
## GET publishers:
Grąžina, pagal nurodytą id, leidyklos informaciją arba, jei id nėra nurodomas, - visų leidyklų informacijos sąrašą.

Galimi atsako kodai:<br>
* 200, 404

Užklausos pavyzdys:<br>
* https://bensuk.azurewebsites.net/publishers/1

Užklausos atsakymas:
```
{
    "id": 1,
    "name": "tai yra name",
    "country": "Lithuania",
    "founded": 1999,
    "isActive": false,
    "nonActiveSince": 2010
}
```

## POST publishers:
Leidžia sukurti naują leidyklą, vartotojas turi būti autorizuotas sukurti naują leidyklą.

Galimi atsako kodai:<br>
* 201, 401

Užklausos pavyzdys:<br>
* https://bensuk.azurewebsites.net/publishers
```
{
    "Name": "testest",
    "Country": "Japan",
    "Founded": "1968"
}
```
Užklausos atsakymas:
```
{
    "id": 19,
    "name": "testest",
    "country": "Japan",
    "founded": 1968,
    "isActive": true,
    "nonActiveSince": null
}
```
## PUT publishers:
Leidžia redaguoti leidyklos informaciją. Galima pakeisti leidyklos neaktyvumo datą(nonActiveSince).

Galimi atsako kodai:<br>
* 200, 401, 403, 404

Užklausos pavyzdys:<br>
* https://bensuk.azurewebsites.net/publishers/1 
```
{
    "nonActiveSince": "2020"
}
```
Užklausos atsakymas:
```
{
    "id": 1,
    "name": "tai yra name",
    "country": "Lithuania",
    "founded": 1999,
    "isActive": false,
    "nonActiveSince": 2010
}
```
## DELETE publishers:
Pašalina, pagal nurodytą id, leidyklą. Leidyklą gali pašalinti tik autorizuoti vartotojai – administratorius arba vartotojas, kuris yra sukūręs leidyklą.

Galimi atsako kodai:<br>
* 204, 401, 403, 404

Užklausos pavyzdys:<br>
* https://bensuk.azurewebsites.net/publishers/1 
