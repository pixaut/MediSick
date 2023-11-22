#include <fstream>
#include <vector>
#include <iostream>
#include <unordered_set>
#include <string>

/* programm that generate batch of tests */

struct Bindings
{
	/* struct that help in generating of tests */

	char Gender;
	std::vector<int> Sicks;

	int size(){
		return Sicks.size(); // returning size of Sicks vector
	}

	void push_back(int value){
		Sicks.push_back(value); // pushing value to back of sicks vector
	}

	int operator[](int index){
		return Sicks[index]; // overloading the [] operator that return Sicks vector value
	} 
};

int N, n, c;
std::string test;
std::unordered_set<int> s; // unordered set helps with generating tests and work fast
std::vector < Bindings > v; // vector with description of sicks


std::string GetTest() {

	/* function that generate test */

	s.clear();
	test.clear(); // clearing before generating

	char gender;
	int i = rand() % v.size(); // selecting of sick

	if(rand()%2 == 0){ // selecting gender
		gender = 'm';
	}else{
		gender = 'w';
	}

	

	if(v[i].Gender != 'n'){ // gender conflict checking
		while(v[i].Gender != 'n' && v[i].Gender != gender){
			i = rand() % v.size();
		}
	}


	int k = v[i].size() / 2 + rand() % (v[i].size() / 2) + 1; // setting required limit as n/2 and hier

	while (s.size() < k) { // generating test while the required limit has been reached
		s.insert(v[i][rand()%v[i].size()]);
	}

	test += gender; // constructing of test
	test += ' ' + std::to_string(k) + '\t';
	for (auto j : s) {
		test += std::to_string(j+1) + ' ';
	}
	test += std::to_string(i + 1);

	return test; // returning test

}


int main() {

	

	srand(time(NULL));

	std::ifstream fin("..\\Sicks\\SicksList.txt");
	std::ofstream fout("..\\TrainingTests\\RandomTests.txt");

	char gender;

	// getting information of sicks from this list

	fin >> N >> c;

	v.resize(N);

	for (int i = 0; i < N; i++) {

		fin >> gender;
		fin >> n;

		v[i].Gender = gender;
		for (int j = 0; j < n; j++) {
			fin >> c;
			v[i].push_back(c-1);
		}
		fin >> c;
	}




	int m;

	std::cout << "Number of tests: "; 
	std::cin >> m;// getting size of batch

	fout << m << '\n'; // writing size of batch

	for (int i = 0; i < m; i++) {
		fout << GetTest() << '\n'; // filling batch with tests
	}

	fin.close();
	fout.close();
}