<!DOCTYPE html >
<html lang="en">
	<head>
		<meta charset="utf-8">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">

		<title>Logic-Game table</title>

		<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/css/bootstrap.min.css" integrity="sha384-rwoIResjU2yc3z8GV/NPeZWAv56rSmLldC3R/AZzGRnGxQQKnKkoFVhFQhNUwEyJ" crossorigin="anonymous">
		
		<style>
			body {
				margin-top: 20px;
			}
			table {
				margin-top: 20px;
			}
		</style>
	</head>

	<body>

		<div class="container">
			<div class="row">
				<div class="col-md-4">
					<h2>Level 1</h2>
					<div id="level1"></div>
				</div>
				<div class="col-md-4">
					<h2>Level 2</h2>
					<div id="level2"></div>
				</div>
				<div class="col-md-4">
					<h2>Level 3</h2>
					<div id="level3"></div>
				</div>
			</div>
			<div class="row">
				<div class="col-md-6">
					<h2>Level 4</h2>
					<div id="level4"></div>
				</div>
				<div class="col-md-6">
					<h2>Level 5</h2>
					<div id="level5"></div>
				</div>
			</div>
		</div>

		<script src="https://code.jquery.com/jquery-3.2.1.min.js" integrity="sha256-hwg4gsxgFZhOsEEamdOYGBf13FyQuiTwlAQgxVSNgt4=" crossorigin="anonymous"></script>
		<script src="https://cdnjs.cloudflare.com/ajax/libs/tether/1.4.0/js/tether.min.js" integrity="sha384-DztdAPBWPRXSA/3eYEEUWrWCy7G5KFbe8fFjk5JAIxUYHKkDx6Qin1DkWx51bBrb" crossorigin="anonymous"></script>
		<script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-alpha.6/js/bootstrap.min.js" integrity="sha384-vBWWzlZJ8ea9aCX4pEW3rVHjgjt7zpkNpZk+02D9phzyeVkE+jo0ieGizqPLForn" crossorigin="anonymous"></script>
		<script>
			function getLevel(level){
				$.getJSON("storage/level"+level+".json?random=<?php echo uniqid(); ?>", function(data) {
					var thead = "<thead><tr><th>#</th><th>Player</th><th>Steps</th></tr></thead>";
					var items = [];
					$.each(data, function(key, val) {
						items.push("<tr><th scope=\"row\">"+(key+1)+"</th><td>"+val.player+"</td><td>"+val.steps+"</td></tr>");
					});
					var tbody = "<tbody>"+items.join("")+"</tbody>";
				 
					$("<table/>", {
						"class": "table table-hover",
						html: thead+tbody
					}).appendTo("#level"+level);
				});
			}
			for (var i = 1; i <= 5; i++){
				getLevel(i);
			}
		</script>
	</body>
</html>
