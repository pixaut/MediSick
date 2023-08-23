from PIL import Image #pillage
from numpy import asarray

img = Image.open('../../programming/ai alphabet/images/image_1.png')

path = 'assets\\images\\letter'

image = asarray(img)

weight,height = 64

arr = [[0]*weight for i in range(height) ]

print("Сколько будет изображений?")

n = int(input())

for k in range(n):

	img = Image.open(path + str(k) + '.png')

	img.show()

	for i in range(weight):
		for j in range(height):
			if(image[i][j][0] != 255 and image[i][j][1] != 255 and image[i][j][2] != 255):
				arr[i][j] = 1

	img.close()
print(*arr, sep = '\n')