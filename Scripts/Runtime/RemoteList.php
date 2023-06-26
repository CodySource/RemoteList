<?php 
const FILTERS = array('key'=>array('filter1','filter2'));
if (!isset($_POST['key'])||!array_key_exists($_POST['key'],FILTERS)) o(null,'Invalid key supplied.');
if (!isset($_POST['f'])||$_POST['f']=='') o(null,'No filter supplied.');
if (preg_match('/[^a-zA-Z0-9 _.\\/*-]/', $_POST['f'])||!in_array($_POST['f'],FILTERS[$_POST['key']])) o(null,'Invalid filter supplied.');
o(array_map(fn($v)=>basename($v),glob($_POST['f'])),null);
function o($c,$e){die(json_encode((object)array('contents'=>$c,'error'=>$e)));}
?>