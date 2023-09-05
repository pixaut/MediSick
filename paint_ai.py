import pygame

pygame.init()
window = pygame.display.set_mode((900, 900))
pygame.display.set_caption("Paint/ai v. 1.5")
clock = pygame.time.Clock()


WHITE = (255, 255, 255)
BLACK = (0, 0, 0)
GRAY = (156, 156, 156)
RED = (210, 30, 30)
GREEN = (77, 209, 29)
BLUE = (20, 179, 228)
PURPLE = (204, 204, 255)


rect1 = pygame.Rect((80, 780, 300, 100))
rect2 = pygame.Rect((500, 780, 300, 100))
rect3 = pygame.Rect((120, 50, 640, 640))


pygame.font.init()
font1 = pygame.font.SysFont("Arial", 60)
font2 = pygame.font.SysFont("Arial", 40)
savefilepic = font1.render("PUSH", False, (0, 0, 0))
clearpic = font1.render("CLEAR", False, (0, 0, 0))


app = True
matrix = [[0 for j in range(65)] for i in range(65)]


def drawGrid():
    blockSize = 10
    for x in range(120, 760, blockSize):
        for y in range(50, 690, blockSize):
            rect = pygame.Rect(x, y, blockSize, blockSize)
            pygame.draw.rect(window, GRAY, rect, 1)


def drawsquare(x, y):
    rect = pygame.Rect((x * 10), (y * 10), 10, 10)
    pygame.draw.rect(window, BLACK, rect, 5)


def drawgui():
    window.fill(PURPLE)
    drawGrid()
    pygame.draw.rect(window, BLUE, rect1, 50)
    pygame.draw.rect(window, BLACK, rect1, 2)
    pygame.draw.rect(window, RED, rect2, 50)
    pygame.draw.rect(window, BLACK, rect2, 2)
    pygame.draw.rect(window, BLACK, rect3, 2)
    window.blit(savefilepic, (155, 795))
    window.blit(clearpic, (560, 795))


def clear():
    drawgui()


drawgui()
while app:
    clock.tick(240)
    pressed = pygame.mouse.get_pressed()
    pos = pygame.mouse.get_pos()
    print(pos)
    for event in pygame.event.get():
        if event.type == pygame.QUIT:
            app = False

    if (pos[0] > 120 and pos[0] < 755) and (pos[1] > 50 and pos[1] < 685):
        if pressed[0]:
            matrix[round((pos[0] - 120) / 10)][round((pos[1] - 50) / 10)] = 1
            print(round((pos[0] - 120) / 10), " ", round((pos[1] - 50) / 10))
            drawsquare(round((pos[0]) / 10), round((pos[1]) / 10))

    if (
        pressed[0]
        and (pos[0] > 80 and pos[0] < 380)
        and (pos[1] > 780 and pos[1] < 880)
    ):
        print(matrix)
        break
    if (
        (pressed[0])
        and (pos[0] > 500 and pos[0] < 800)
        and (pos[1] > 780 and pos[1] < 880)
    ):
        clear()

    pygame.display.flip()
