<?php
if ($_POST["type"] === "new_player")
{
	echo $_POST["name"];
	echo ", ";
	echo $_POST["country"];
	echo ", ";
	echo $_POST["age"];
	echo ", ";
	echo $_POST["gender"];
	echo ", ";
	echo $_POST["time"];
}
else if ($_POST["type"] === "new_session")
{
	echo $_POST["player_id"];
	echo ", ";
	echo $_POST["time"];
}
else if ($_POST["type"] === "end_session")
{
	echo $_POST["session_id"];
	echo ", ";
	echo $_POST["time"];
}
else if ($_POST["type"] === "buy_item")
{
	echo $_POST["item_id"];
	echo ", ";
	echo $_POST["session_id"];
	echo ", ";
	echo $_POST["time"];
}


?>