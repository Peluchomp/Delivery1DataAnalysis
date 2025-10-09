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

$sql = "INSERT INTO Sessions (UserID, SessionID, LengthOfSession, Time)
VALUES (0, 0, 20, '2025-10-9 09:38:23.230')";

if ($conn->query($sql) === TRUE) {
	echo "New record created successfully";
} else {
	echo " Error: " . $sql . "<br>" . $conn->error;
}
?>