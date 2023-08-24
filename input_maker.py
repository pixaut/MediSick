import cv2

f = open('assets\\lib.txt', 'w')
path = 'assets\\images\\letter0.png'
img = cv2.imread(path, cv2.IMREAD_GRAYSCALE)
w = img.shape[0]
h = img.shape[1]

for i in range(w):
     for j in range(h):
         a = img[i, j]
         ans = round( (255-a)/255, 1)
         f.write(str(ans))
         f.write(' ')
     f.write('\n')

f.close()



