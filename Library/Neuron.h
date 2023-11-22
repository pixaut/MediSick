#pragma once
#include <cmath>

class Neuron
{
public:
	double value, error, ActiveValue;

	void Activate() {
		/* activation function */
		ActiveValue = 1.0 / (1.0 + exp(-value));
	}
	double DerivationVal() {
		/* function that returnning derivative value*/ 
		return ActiveValue * (1.0 - ActiveValue);
	}

};

