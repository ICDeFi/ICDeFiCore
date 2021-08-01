var W = window.innerWidth;
	var H = window.innerHeight;

	// Update new screen size when resize
	$( window ).resize(function() {
	  W = window.innerWidth;
	  H = window.innerHeight;
	});

	var particles = [];

	var _config = {
	  particle_count: 10,
	  minDelay: 1,
	  maxDelay: 60,
	  minDiameter: 5,
	  maxDiameter: 140,
	  delayMultiples: 50
	};

	function getRandom(min, max) {
	  return Math.random() * (max - min) + min;
	}

	function Particle() {
	  this.id = ''; 
	  this.x = function() {
		return getRandom(0, W);
	  };
	  this.y = function() {
		return getRandom(0, H);
	  };
	  this.diam = function() {
		return getRandom(_config.minDiameter, _config.maxDiameter);
	  };
	  this.delay = function() {
		return getRandom(_config.minDelay, _config.maxDelay) * _config.delayMultiples;
	  };
	}

	Particle.prototype.start = function() {
	  var newPar = this.createNewParticle();
	  this.addParticle(newPar);
	  return 1;
	};

	Particle.prototype.createNewParticle = function() {
	  var newPar = document.createElement('div');
		newPar.setAttribute('id', this.id);
		newPar.setAttribute('class', 'loading-icon');
		newPar.style.width = newPar.style.height = this.diam() + 'px';
		newPar.style.left = this.x() + 'px';
		newPar.style.top = this.y() + 'px';
		return newPar;
	};

	Particle.prototype.addParticle = function(newPar) {
	  var self = this;
		setTimeout(function(){
		  $('body').append(newPar);
		  self.move();
		}, self.delay());
	  return 1;
	};

	// Move to new position after 4 seconds
	// Get new position to update
	Particle.prototype.move = function() {
		var self = this;
		var id = this.id;
		var newLeft = this.x();
		var newTop = this.y();
		var newWidth, newHeight;
		newWidth = newHeight = this.diam();
		setTimeout(function() {
		  var currentPar = $('#' + id);
		  currentPar.css({top: newTop, left: newLeft, width: newWidth, height: newHeight});
		  self.move();
		}, 4000);
	  return 1;
	};

	// Create praticles list
	for (var i = 0; i < _config.particle_count; i++) {
	  var particle = new Particle(i);
	  particles.push(particle);
	}

	particles.forEach(function(particle) {
	  var id = particles.indexOf(particle);
	  particle.id = 'particle-' + id;
	  var delay = particle.delay();
	  particle.start();
	});

	$(window).focus(function() {
	  console.log("Focus");
	});

	$(window).blur(function() {
	  console.log("Blur");
	  console.log(particles);
	});