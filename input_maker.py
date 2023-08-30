import cv2
import os.path
import os

f = open('assets\\lib.txt', 'w') # открываем файл для записи

for k in range(26): # проходимся по каждой букве

    l = 0
    
    path = 'assets\\images\\letter'+str(chr(ord('a') + k)) # путь к букве

    path2 = path +'0.png'

    while(os.path.exists(path2)):

        l = l + 1

        img = cv2.imread(path2, cv2.IMREAD_GRAYSCALE) # записываем картинку в переменную
        w,h = 64,64# ширина высота

        for i in range(w):
            for j in range(h):
                a = img[i, j] # параметр пикселя
                ans = round((255 - a) / 255, 1) # 1 - полностью черный, 0 - полностью белый
                f.write(str(ans)) # записываем параметр пикселя в файл
                f.write(' ')
            f.write('\n')  # разделители
        f.write(str(chr(ord('a') + k)))
        f.write('\n')
        path2 = path + str(l) + '.png'

f.close() # закрыть файл


