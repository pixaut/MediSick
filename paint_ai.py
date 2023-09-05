app = True
import pygame
pygame.init() #инициализация pygame
window = pygame.display.set_mode((900,900)) #установка размеров окна
pygame.display.set_caption("draw a letter") #присвоение название игре  
clock = pygame.time.Clock()
WHITE = (255, 255, 255) 
BLACK = (0, 0, 0) 
GRAY = (156, 156, 156)
rect1 = pygame.Rect((80, 780, 300, 100))
rect2 = pygame.Rect((500, 780, 300, 100))
rect3 = pygame.Rect((120, 50, 640, 640))
pygame.font.init()
font1 = pygame.font.SysFont('Arial', 60)
font2 = pygame.font.SysFont('Arial', 40)
savefilepic = font1.render('SAVE', False, (0, 0, 0))
clearpic = font1.render('CLEAR', False, (0, 0, 0))
def drawGrid():
    blockSize = 10
    for x in range(120, 760, blockSize):
        for y in range(50, 690, blockSize):
            rect = pygame.Rect(x, y, blockSize, blockSize)
            pygame.draw.rect(window, GRAY, rect, 1)
matrix = [[0 for j in range(64)] for i in range(64)]
print(matrix)
window.fill(WHITE) 
drawGrid()
pygame.draw.rect(window, BLACK, rect1, 4)
pygame.draw.rect(window, BLACK, rect2, 4)
pygame.draw.rect(window, BLACK, rect3, 2)
window.blit(savefilepic, (155,795))
window.blit(clearpic, (560,795))
while app:
    clock.tick(120)
    pressed = pygame.mouse.get_pressed()
    pos = pygame.mouse.get_pos()
    #print(pos)
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            app = False
    


    
    
    if (pos[0] > 120 and pos[0] < 760) and( pos[1] > 50 and pos[1] < 690):
        if (pressed[0]):
            matrix[round((pos[0]-120)/10)][round((pos[1]-50)/10)] = 1
            print(round((pos[0]-120)/10)," ",round((pos[1]-50)/10))
        
                
            rect4 = pygame.Rect((pos[0], pos[1], 10, 10))
            
            pygame.draw.rect(window, BLACK, rect4, 5)


    if pressed[0] and (pos[0] > 80 and pos[0] < 380) and( pos[1] > 780 and pos[1] < 880):
        print(matrix)
        break

        
    
    
    pygame.display.flip() 

    
    
    
    
   

    

   


        
        
        
        
        








    

