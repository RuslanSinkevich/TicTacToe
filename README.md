## TicTacToe
### REST API для игры в крестики нолики

Данный REST API имеет два POST метода
#### 1)	POST метод (/game-start) 
– данный метод возвращает json объект который служит каркасом для старта игры.

##### Параметры json ответа
- idGame –  id конкретной игры (guid)
- nameGame – имя игры (string)
- idPlayer – id игрока, который сделал ход (int)
- idPlayerNext – id игрока, изменяется после каждого хода   
    либо 1-(1 игрок) или 9-(второй игрок) (int)
- idPlayerWin – id игрока, который выиграл (int)
- data  –  содержит значения каждой ячейки, изменяется после каждого хода либо 1-(1 игрок) или 9-(второй игрок) (array)

##### Request URL:  localhost/game-start
##### Response body:
```json
{
  "idGame": "c5a17b05-9c61-4ad7-879a-f7db7f8b1340",
  "nameGame": "Крестики нолики",
  "idPlayer": 1,
  "idPlayerNext": 1,
  "idPlayerWin": 0,
  "data": [
    -1,
    -1,
    -1,
    -1,
    -1,
    -1,
    -1,
    -1,
    -1
  ]
} 
```

#### 2)	POST метод (/game-flow)
Данный метод передаёт изменённый во время игры json который мы получили вызывая метод (/game-start).     
При каждом вызове метода необходимо изменять согласно хода игрока один из элементов массива data,   
объекта json (посредством JavaScript).   
После изменения отправляем json обьект методом POST на адрес (/game-flow).   
Получаем ответ и смотрим если idPlayerWin имеет значение больше 0 то заканчиваем игру.


#### Каждый раз отправляя запрос (/game-flow) вы должны изменять: 
1)	idPlayerNex – указывая id игрока сделавшего ход это либо 1-(1 игрок) либо 9-(второй игрок)
2)	data - Изменить ячейку массива, значение ячейки равно id игрока чей ход.


#### Пример HTML реализации для игры.   
В данном примере использованы CSS фреймворк tailwind и JS библиотека alpine.

```html
<head>
    <link href="/css/output.css" rel="stylesheet" />
    <script defer src="https://cdn.jsdelivr.net/npm/alpinejs@3.x.x/dist/cdn.min.js"></script>
</head>

<body class="mx-auto max-w-[1250px] text-sm">
    <div x-data="" x-init="startGame()" class="mt-7 flex w-full items-center justify-center">
        <fieldset class="rounded border border-solid border-gray-300 p-4 shadow-xl">
            <legend class="font-medium " id="NameGame">Крестики нолики</legend>
            <div class="grid grid-cols-3 gap-4">
                <div x-on:click="gameFlow($refs.id_0)" x-ref="id_0" id_game="0" class="input_game"></div>
                <div x-on:click="gameFlow($refs.id_1)" x-ref="id_1" id_game="1" class="input_game"></div>
                <div x-on:click="gameFlow($refs.id_2)" x-ref="id_2" id_game="2" class="input_game"></div>
                <div x-on:click="gameFlow($refs.id_3)" x-ref="id_3" id_game="3" class="input_game"></div>
                <div x-on:click="gameFlow($refs.id_4)" x-ref="id_4" id_game="4" class="input_game"></div>
                <div x-on:click="gameFlow($refs.id_5)" x-ref="id_5" id_game="5" class="input_game"></div>
                <div x-on:click="gameFlow($refs.id_6)" x-ref="id_6" id_game="6" class="input_game"></div>
                <div x-on:click="gameFlow($refs.id_7)" x-ref="id_7" id_game="7" class="input_game"></div>
                <div x-on:click="gameFlow($refs.id_8)" x-ref="id_8" id_game="8" class="input_game"></div>
            </div>
            <div id="win_game" class="hidden mt-4 justify-center font-medium  text-red-500"></div>
        </fieldset>

    </div>
</body>
    
<script>
    let game = null;

    function gameFlow(item) {
        let idGame = item.getAttribute("id_game");
        if (game.idPlayerWin > 0)
            return;

        if (game.data[idGame] < 0)
            game.data[idGame] = game.idPlayerNext;
        else return;

        game.idPlayer = game.idPlayerNext

        if (game.idPlayerNext == 1) {
            item.innerHTML = "O";
            game.idPlayerNext = 9;
        } else {
            item.innerHTML = "X";
            game.idPlayerNext = 1;
        }

        getGame(game);
    }

    function gameWin(idPlayerWin) {
        let element = document.getElementById("win_game");
        if (idPlayerWin == 1)
            element.innerHTML = "победил игрок 1";
        else
            element.innerHTML = "победил игрок 2";
        element.classList.remove("hidden");
        element.classList.add("flex");
    }

    async function startGame() {
        let resp = await fetch("./game-start", {
            method: "POST",
            headers: {
                "Content-Type": "application/json;charset=utf-8",
            },
        })
            .then((response) => response.json())
            .then((data) => data);
        game = resp;
    }

    async function getGame(item) {
        let resp = await fetch("./game-flow", {
            method: "POST",
            headers: {
                "Content-Type": "application/json;charset=utf-8",
            },
            body: JSON.stringify(item),
        })
            .then((response) => response.json())
            .then((data) => data);
        game = resp;
        if (game.idPlayerWin > 0) {
            gameWin(game.idPlayerWin)
        }
    }
</script>
```

[пример реализации](http://185.46.11.197:8080/index.html)
