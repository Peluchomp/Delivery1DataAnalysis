<?php
$servername = "localhost";
$username = "paums3";
$password = "mbmQcfjTKPmm";

//Create connection
$conn = new mysqli($servername, $username, $password, "paums3");

//Check connection

if ($conn->connect_error) {

die("Connection failed: " . $conn->connect_error);
}
echo "Connected succcessfully";

$userid = $_POST["UserID"];
$sesionid = $_POST["SessionID"];
$lengthofsession = $_POST["LengthOfSession"];
$time = $_POST["Time"];


$sql = "INSERT INTO Sessions (UserID, SessionID, LengthOfSession, Time)
VALUES ('$userid', '$sesionid', '$lengthofsession', '$time')";

if ($conn->query($sql) === TRUE) {
	echo "New record created successfully";
} else {
	echo " Error: " . $sql . "<br>" . $conn->error;
}
?>