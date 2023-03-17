<?php 
$key=file_get_contents('key.txt');
if ($key&&(!isset($_POST['key'])||$_POST['key']!=$key)) o(null,'Invalid key supplied.');
if (!isset($_POST['f'])||$_POST['f']=='') o(null,'No filter supplied.');
if (preg_match('/[^a-zA-Z0-9 _.\\/*-]/', $_POST['f'])) o(null,'Invalid filter supplied.');
o(array_map(fn($v)=>basename($v),glob($_POST['f'])),null);
function o($c,$e){die(json_encode((object)array('contents'=>$c,'error'=>$e)));}
?>