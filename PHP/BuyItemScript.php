<?php
$servername = "localhost";
$username = "paums3";
$password = "mbmQcfjTKPmm";

// Create Connection
$conn = new mysqli($servername, $username, $password, "paums3");

// Check Connection
if ($conn->connect_error)
{
	die("Connection failed:" . $conn->connect_error);
}

$sessionID = $_POST["session_id"];
$time = $_POST["time"];
$itemID = $_POST["item_id"];


$sql = "INSERT INTO Revenues (SessionID, Time, ItemID)
VALUES ('$sessionID', '$time', '$itemID')";

if ($conn->query($sql) === TRUE)
{
	$last_id = $conn->insert_id;
	echo $last_id;
} else {
	echo "Error: " . $sql . "<br>" . $conn->error;
}
?>