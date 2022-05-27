extends TextureRect




func _ready():
	$Cooldown.hide()
	$Black.hide()

func _process(delta):
	if $Timer.is_stopped():
		$Cooldown.hide()
		$Black.hide()
		
	else:
		$Cooldown.show()
		$Black.show()

		
	var text = int($Timer.time_left) + 1
	$Cooldown.text = String(text)



