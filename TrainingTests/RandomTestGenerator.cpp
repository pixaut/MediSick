#include <fstream>
#include <vector>
#include <iostream>
#include <unordered_set>
#include <string>
/*WARNING gender update*/
struct Bindings
{
	int SimInd;
	char Gender;
	std::vector<int> Sicks;

	int size(){
		return Sicks.size();
	}

	void push_back(int value){
		Sicks.push_back(value);
	}

	int operator[](int index){
		return Sicks[index];
	} 
};

int N, n, c;
std::string ans;
std::unordered_set<int> s;
std::vector < Bindings > v;


std::string GetTest() {

	s.clear();
	ans.clear();

	char gender;
	int i = rand() % v.size();

	if(rand()%2 == 0){
		gender = 'm';
	}else{
		gender = 'w';
	}

	

	if(v[i].Gender != 'n'){
		while(v[i].Gender != 'n' && v[i].Gender != gender){
			i = rand() % v.size();
		}
	}


	int k = v[i].size() / 2 + rand() % (v[i].size() / 2) + 1;

	while (s.size() < k) {
		s.insert(v[i][rand()%v[i].size()]);
	}

	ans += gender;

	ans += ' ' + std::to_string(k) + '\t';

	for (auto j : s) {
		ans += std::to_string(j+1) + ' ';
	}

	ans += std::to_string(i + 1);

	return ans;

}


int main() {

	srand(time(NULL));

	std::ifstream fin("..\\Sicks\\SicksList.txt");
	std::ofstream fout("RandomTests.txt");

	char gender;

	fin >> N;

	v.resize(N);

	for (int i = 0; i < N; i++) {

		fin >> gender;
		fin >> n;

		v[i].Gender = gender;
		for (int j = 0; j < n; j++) {
			fin >> c;
			v[i].push_back(c-1); //чтобы было от нуля
		}
		fin >> c;
	}

	int m;

	std::cout << "Number of tests: ";
	std::cin >> m;

	fout << m << '\n';

	for (int i = 0; i < m; i++) {
		fout << GetTest() << '\n';
	}

	fin.close();
	fout.close();
}