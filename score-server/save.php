<?php
	$_MAX_RECORDS = 5;
	
	if (!empty($_POST)) {
		$req = null;
		$req['player'] = htmlspecialchars($_POST["player"]);
		$req['level'] = intval($_POST["level"]);
		$req['steps'] = intval($_POST["steps"]);
		
		$fileName = 'storage/level'.$req['level'].'.json';

		$table = array();
		if (file_exists($fileName)){
			$table = json_decode(file_get_contents($fileName), true);
		}

		$record = null;
		$record['player'] = $req['player'];
		$record['steps'] = intval($req['steps']);
		
		$tableCount = count($table);
		
		if ($tableCount < 1) {
			array_push($table, $record);
		} else {
			$added = false;
			for($i = 0; $i < $tableCount; $i++) {
				if ($table[$i]['steps'] >= $record['steps']) {
					array_splice($table, $i, 0, array($record));
					$added = true;
					break;
				}
			}
			if (!$added && $tableCount < $_MAX_RECORDS) {
				array_push($table, $record);
			}
			if ($added && $tableCount >= $_MAX_RECORDS) array_pop($table);
		}
		
		file_put_contents($fileName, json_encode($table));
		echo 'OK';
	} else {
		echo 'FAIL';
	}
?>