Zbuduj minutnik w tym projekcie Avalonia. Ma odmierzać 25 minut pracy,
potem 5 minut przerwy, i liczyć skończone cykle. Obie długości mają być do
zmiany w oknie. Jeden przycisk start/pauza, drugi reset. Czas widoczny jako
mm:ss, licznik cykli widoczny obok. Po skończonej pracy przerwa ma ruszyć
sama. Interfejs po polsku, nazwy w kodzie po angielsku, bez dodatkowych
bibliotek. Cała logika wprost w pliku MainWindow.axaml.cs, bez osobnych
klas pośredniczących.

wlasna reka: historia cykli - 23 minuty
data: 22.10.2026
narzędzie: gemini 3.8 flash
tokeny po polsku: 155
tokeny po angielku: 105

W przypadku wpisaniu w długość pracy 25,5 czas pracy zaokrągił się na 26 minut lecz minutnik wystartował na 25 minut
W przypadku wpisania 25.5 czas pracy zaokrągił się na 24 minuty lecz minutnik nadal wystartował na 25 minut

## Analiza promptu

Prompt zawiera zarówno informacje o tym, co program ma robić, jak i o sposobie jego wykonania.

**Co ma robić program:**
- Odmierzać 25 minut pracy i 5 minut przerwy oraz liczyć ukończone cykle.
- Pozwalać na zmianę długości pracy i przerwy w oknie.
- Udostępniać przycisk start/pauza oraz osobny reset.
- Automatycznie rozpoczynać przerwę po zakończeniu pracy.

**Jak ma być zrobiony:**
- Projekt wykorzystuje Avalonia.
- Interfejs jest po polsku, a nazwy w kodzie po angielsku.
- Program nie korzysta z dodatkowych bibliotek.
- Cała logika znajduje się w `MainWindow.axaml.cs`, bez klas pośredniczących.

**Po czym poznam, że działa:**
- Czas wyświetla się w formacie `mm:ss`, a licznik cykli jest widoczny obok.

Podsumowując, w promptcie najwięcej informacji dotyczy tego, co program ma robić (4), następnie sposobu wykonania (3). Najmniej jest kryteriów pozwalających rozpoznać gotowy program (1).

### Zachowanie przycisku Reset

Reset zatrzymuje minutnik, wraca do trybu pracy i ustawia początkowy czas pracy. Zeruje licznik ukończonych cykli, czyści historię i wyświetla komunikat o gotowości do pracy.

Takie zachowanie jest decyzją modelu, ponieważ oryginalny prompt nie określał, co Reset ma robić w trakcie przerwy.