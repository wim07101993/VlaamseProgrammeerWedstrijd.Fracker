# Fracking
Een opgave uit de vlaamse programmeerwedstrijd uit 2017 (https://www.vlaamseprogrammeerwedstrijd.be/2018/opgaven/2018/cat1/fracking/fracking.pdf)
	 
## Opgave
Na jarenlang financieel wanbeheer staat het sympathieke bergdorp Heuven aan de rand van het bankroet. De Heuvenaren hebben daarom besloten om nieuwe fondsen te genereren via fracking. Fracking is een proces waarbij aardgas ontgonnen wordt door de injectie van water onder hoge druk in de ondergrond. De ondergrond van Heuven is echter niet volledig geschikt voor deze techniek aangezien hij bestaat uit harde en zachte onderdelen. Per dag dat er gefrackt wordt, worden harde delen die horizontaal of verticaal grenzen aan zachte delen omgezet naar zachte delen. Wanneer de bovenste grondlaag niet meer via harde delen geconnecteerd is aan de onderste grondlaag volgt een instorting. Gevraagd wordt om een algoritme op te stellen dat berekent hoeveel dagen het duurt voordat een instorting optreedt.
Het kan ook voorkomen dat dit aantal gelijk is aan 0.
In de opgave wordt de ondergrond voorgesteld door een matrix van sterren en punten. Sterren stellen harde ondergrond voor, punten stellen zachte ondergrond voor. In de twee onderstaande voorbeelden is de ondergrond stabiel. De bovenste rij in de matrix is verbonden met de onderste rij via sterren die horizontaal of verticaal aan elkaar grenzen.
#### Voorbeeld1
```
*****..***...
*****.*****..
*****.*****..
.***..*****..
```
#### Voorbeeld2
```
.......*
.*****.*
.*...*.*
.***.***
...*....
```
Per dag wordt een ster die horizontaal of verticaal grenst aan een punt omgezet in een punt. Na dag 1 ziet de ondergrond van voorbeeld1 er als volgt uit:
```
****....*....
****...***...
.***...***...
..*....***...
```
Zoals hieronder getoond, volgt na dag 2 een instorting:
```
***..........
.**.....*....
..*.....*....
........*....
```

## Invoer
De eerste regel van de invoer bevat het aantal testgevallen (maximaal 150.) Per testgeval volgt:
 - één regel met N rijen, met N een geheel getal tussen 1 en 20; 
 - één regel met M kolommen, met M een geheel getal tussen 1 en 20;
 - N rijen bestaande uit M keer een ster of een punt. 

Een voorbeeld van de invoer wordt hieronder getoond.
#### Voorbeeldinvoer
```
2
4
13
*****..***...
*****.*****..
*****.*****..
.***..*****..
5
6
..**..
..*...
..**..
...*..
...*..
 ```
## Uitvoer
Per testgeval wordt het volgnummer van het testgeval gevolgd door een spatie en het aantal dagen dat het fracking-proces veilig herhaald kan worden. Dit aantal kan gelijk zijn 0.
#### Voorbeelduitvoer
```
1	2
2	1
```
