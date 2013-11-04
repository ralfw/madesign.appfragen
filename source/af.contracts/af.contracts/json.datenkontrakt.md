# Allgemeine Form von Nachrichten:

{
	"cmd": "<kommandoname>",
	"payload" : {
		// wie beim kommando beschrieben
	}
}


# Mit Json umgehen
## Json empfangen, Kommando ausführen

void Process(string json) {
  dynamic obj = json.FromJson();
  switch((string)obj.cmd) {
	case "...":
		...
		break;
  }
}

## Output als Json senden
...
var obj = new ExpandoObject();
obj.cmd = "Beantworten";
obj.payload = new ExpandoObject();
obj.payload.AntwortmoeglichkeitId = "abc";

var json = obj.ToJson();
Json_output(json);


# Vom UI

## Kommando: Beantworten
{
	"AntwortmoeglichkeitId": "<id>"
}

## Kommando: Auswerten
{
}

## Kommando: Fragenkatalog laden
{
	"Dateiname": "<dateiname>"
}

## Kommando: Auswertung beenden
{
}


# Zum UI

## Kommando: Starten
{
}

## Kommando: Fragebogen anzeigen
{
	"Fragen": [
		{
			"Text": "<fragetext>",
			"Antwortmoeglichkeiten": [
				{
					"Id": "<id>",
					"Text": "<antwortmoeglichkeitentext>",
					"Ausgewaehlt": "true", // oder false
				},
				...
			]
		},
		...
	]
}

## Kommando: Auswertung anzeigen
{
	"AnzahlFragen": 10, // Beispieldaten
	"AnzahlRichtig": 3,
	"ProzentRichtig": "0,3",
	"AnzahlFalsch": 1,
	"ProzentFalsch": "0,1",
	"AnzahlWeissNicht": 6,
	"ProzentWeissNicht": "0,6"
}

## Kommando: Auswertung schliessen
{
}
