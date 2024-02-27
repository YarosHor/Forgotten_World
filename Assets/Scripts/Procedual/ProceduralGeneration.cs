using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Linq;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] GameObject enemy1, enemy2, enemy3, saw;
    [SerializeField] int width;
    [SerializeField] int heightActualizator;
    [SerializeField] int dirtHeight;
    [SerializeField] GameObject chest;
    [SerializeField] Tilemap grassTilemap, platformTileMap, stoneTilemap, noPlayerColider, decorations, background;
    [SerializeField] Tile  grass, sideUpGrass, sideDownGrass, cornerUp, cornerDown, connectUp, connectDown, dirt;
    [SerializeField] Tile platform, platformStart, platformEnd;
    [SerializeField] Tile brickLeft, brick, brickRight, brickColumn;
    [SerializeField] Tile grass1, grass2, branch1, branch2, rock, shroom, crate;
    //[SerializeField] Tile crate, door, house1, house2, tree1, tree2, tree3;
    [Range(0,100)]
    [SerializeField] float heightValue, smoothness;
    [SerializeField]float seed;
    void Start()
    {
        seed = Random.Range(-1000000, 1000000);
        Generation();
    }

    private void Update()
    {
    }
    void Generation()
    {
        List<Vector3Int> positions = new List<Vector3Int>();
        int actualHeight = 0;
        //distancia del mapa
        bool falled = false;
        for (int x = 0; x < width; x++)
        {
            /*if (0 == Random.Range(0, 4))
            {

            }
            else
            {

            }*/
                int height = Mathf.RoundToInt (heightValue * Mathf.PerlinNoise(x / smoothness, seed));
            if(x == 0 || x > 384)
            {
                height = 12;
            }
            if (x == 0)
            {
                actualHeight = height + 0;
            }
            int random = Random.Range(0, heightActualizator);
            Debug.Log("--------------------");
            int rand = Random.Range(0, 100);

            if (rand > 95 && falled == false)
            {
                falled = true;
                int initial = x + 0;
                int width = 0;
                x++;
                width++;
                while (Random.Range(0, 3) != 0)
                {
                    x++;
                    width++;
                    if(width >= 9)
                    {
                        break;
                    }
                }
                int final = x + 0;
                if(Random.Range(0, 3) == 0)
                {
                    positions.Add(new Vector3Int(initial, actualHeight, 2));
                }
                if (Random.Range(0, 3) == 0)
                {
                    positions.Add(new Vector3Int(final, actualHeight, -2));
                }
                for (int y = actualHeight + 0; y > 0; y--)
                {
                    if (Random.Range(0, 10) == 0)
                    {
                        decorations.SetTile(new Vector3Int(initial, y, 0), branch1);
                    }
                    if (Random.Range(0, 10) == 0)
                    {
                        decorations.SetTile(new Vector3Int(final, y, 0), branch2);
                    }
                }
            }
            else
            {
                falled = false;
                for (int y = actualHeight + 0; y > 0; y--)
                {
                    grassTilemap.SetTile(new Vector3Int(x, y, 0), dirt);
                }

                if (rand == 0 && x < 380)
                {
                    Debug.Log("Pedestal");
                    for (int i = 0; i < 9; i++)
                    {
                        for (int y = actualHeight + 0; y > 0; y--)
                        {
                            grassTilemap.SetTile(new Vector3Int(x + i, y, 0), dirt);
                        }
                        if (i == 0)
                        {
                            stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1, 0), brickLeft);
                        }
                        else if (i == 8)
                        {
                            stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1, 0), brickRight);
                        }
                        else
                        {
                            stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1, 0), brick);
                        }
                        switch (i)
                        {
                            case 1:
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 1, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 2, 0), brickLeft);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 3, 0), brickLeft);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 4, 0), brickLeft);
                                break;
                            case 2:
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 1, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 2, 0), brickLeft);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 3, 0), brickRight);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 4, 0), brick);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 5, 0), brickLeft);
                                break;
                            case 3:
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 1, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 2, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 3, 0), brickLeft);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 4, 0), brick);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 5, 0), brick);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 6, 0), brickLeft);
                                break;
                            case 4:
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 1, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 2, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 3, 0), brickLeft);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 4, 0), brick);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 5, 0), brick);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 6, 0), brick);
                                Instantiate(chest, new Vector3((float)(x + i + 0.5), actualHeight + 2 + 0.7f, 2), transform.rotation);
                                break;
                            case 5:
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 1, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 2, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 3, 0), brickLeft);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 4, 0), brick);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 5, 0), brick);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 6, 0), brickRight);
                                break;
                            case 6:
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 1, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 2, 0), brickLeft);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 3, 0), brickLeft);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 4, 0), brick);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 5, 0), brickRight);
                                break;
                            case 7:
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 1, 0), brickLeft);
                                background.SetTile(new Vector3Int(x + i, actualHeight + 1 + 2, 0), brickLeft);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 3, 0), brickRight);
                                stoneTilemap.SetTile(new Vector3Int(x + i, actualHeight + 1 + 4, 0), brickRight);
                                break;

                        }

                    }
                    x = x + 8;
                }

                else if (random == 0)
                {
                    /*if (rand > 40)
                    {
                        Debug.Log("Saw:  " + x);
                        Instantiate(saw, new Vector3(x, actualHeight + 1, 1), transform.rotation);
                    }*/

                    /*if (rand > 20 && canSaw)
                    {
                        Debug.Log("Saw:  " + x);
                        Instantiate(saw, new Vector3(x, actualHeight + 1, 1), transform.rotation);
                    }*/
                    int result = 0;
                    Debug.Log(random);
                    //down
                    if (actualHeight > height)
                    {
                        //positions.Add(new Vector3Int(x + 1, actualHeight, 1));
                        int resultUpHeight = 0;
                        for (int y = actualHeight + 0; y > height; y--)
                        {
                            if (y == actualHeight)
                            {
                                Debug.Log("DRUGER");
                                grassTilemap.SetTile(new Vector3Int(x, y, 0), cornerDown);
                            }
                            else
                            {
                                grassTilemap.SetTile(new Vector3Int(x, y, 0), sideDownGrass);
                            }
                            if (Random.Range(0, 4) == 0)
                            {
                                decorations.SetTile(new Vector3Int(x + 1, y, 0), branch1);
                            }
                            resultUpHeight++;
                            result = y;
                        }

                        if (resultUpHeight >= 4)
                        {
                            Debug.Log("frare " + resultUpHeight);
                            Debug.Log("Rugultae: " + (x - 1) + "  " + resultUpHeight);
                            if (resultUpHeight == 4)
                            {

                            }
                            else
                            {
                                if (resultUpHeight <= 7)
                                {
                                    Debug.Log("Mammagrub" + x + ": " + resultUpHeight + " = " + (int)(((float)resultUpHeight / 2)));
                                    positions.Add(new Vector3Int(x + 1, (result - 1) + (resultUpHeight / 2), 2));
                                }
                                else if (resultUpHeight >= 8 && resultUpHeight <= 10)
                                {
                                    Debug.Log("MammagrubA" + x + ": " + resultUpHeight + " = " + (int)((float)resultUpHeight / (float)3) + "  +  " + (int)(((float)resultUpHeight / (float)3) * (float)2));
                                    positions.Add(new Vector3Int(x + 1, (result - 1) + (int)(((float)resultUpHeight / (float)3) * (float)2), 2));
                                    positions.Add(new Vector3Int(x + 1, (result - 1) + (int)((float)resultUpHeight / (float)3), 2));
                                }
                                else if (resultUpHeight > 10)
                                {
                                    Debug.Log("MammagrubB" + x + ": " + resultUpHeight + " = " + (int)((float)resultUpHeight / (float)4) + "  +  " + (int)(((float)resultUpHeight / (float)4) * (float)2));
                                    positions.Add(new Vector3Int(x + 1, (result - 1) + ((resultUpHeight / 4) * 2), 2));
                                    positions.Add(new Vector3Int(x + 1, (result - 1) + ((resultUpHeight / 4) * 3), 2));
                                    positions.Add(new Vector3Int(x + 1, (result - 1) + (resultUpHeight / 4), 2));

                                }

                            }
                        }
                        actualHeight = result - 1;
                        grassTilemap.SetTile(new Vector3Int(x, actualHeight, 0), connectDown);
                    }
                    //top
                    else if (actualHeight < height)
                    {
                        int resultDownHeight = 0;
                        for (int y = actualHeight + 0; y < height; y++)
                        {
                            if (y == actualHeight)
                            {
                                Debug.Log("DRUGER");
                                grassTilemap.SetTile(new Vector3Int(x, y, 0), connectUp);
                            }
                            else
                            {
                                grassTilemap.SetTile(new Vector3Int(x, y, 0), sideUpGrass);
                                if (Random.Range(0, 4) == 0)
                                {
                                    decorations.SetTile(new Vector3Int(x - 1, y, 0), branch2);
                                }
                            }
                            resultDownHeight++;
                            result = y;
                        }
                        if (resultDownHeight >= 4)
                        {
                            Debug.Log("Regult: " + (x - 1) + "  " + resultDownHeight);
                            if (resultDownHeight == 4)
                            {

                            }
                            else
                            {
                                if (resultDownHeight <= 7)
                                {
                                    positions.Add(new Vector3Int(x - 1, (result) - (resultDownHeight / 2), -2));
                                }
                                else if (resultDownHeight >= 8 && resultDownHeight <= 10)
                                {
                                    Debug.Log("Mammamia: " + resultDownHeight + " = " + (int)((float)resultDownHeight / (float)3) + "  +  " + (int)(((float)resultDownHeight / (float)3) * (float)2));
                                    positions.Add(new Vector3Int(x - 1, (result + 0) - (int)(((float)resultDownHeight / (float)3) * (float)2), -2));
                                    positions.Add(new Vector3Int(x - 1, (result + 0) - (int)((float)resultDownHeight / (float)3), -2));
                                }
                                else if (resultDownHeight > 10)
                                {
                                    Debug.Log("Mammamia2: " + resultDownHeight + " = " + (int)((float)resultDownHeight / (float)4) + "  +  " + (int)(((float)resultDownHeight / (float)4) * (float)2));
                                    positions.Add(new Vector3Int(x - 1, (result + 0) - ((resultDownHeight / 4) * 2), -2));
                                    positions.Add(new Vector3Int(x - 1, (result + 0) - ((resultDownHeight / 4) * 3), -2));
                                    positions.Add(new Vector3Int(x - 1, (result + 0) - (resultDownHeight / 4), -2));

                                }

                            }
                        }
                        positions.Add(new Vector3Int(x - 1, actualHeight, -1));
                        actualHeight = result + 1;
                        grassTilemap.SetTile(new Vector3Int(x, actualHeight, 0), cornerUp);
                        if (Random.Range(0, 4) == 0)
                        {
                            decorations.SetTile(new Vector3Int(x + 1, actualHeight, 0), branch1);
                        }


                    }
                    else
                    {
                        grassTilemap.SetTile(new Vector3Int(x, actualHeight, 0), grass);
                        GenerateDecorations(x, actualHeight + 1);
                        
                    }



                }
                else
                {
                    if (rand < 10 && grassTilemap.HasTile(new Vector3Int(x - 2, actualHeight, 0)))
                    {
                        Debug.Log("Saw:  " + x);
                        Instantiate(saw, new Vector3(x, actualHeight + 1, 1), transform.rotation);
                    }
                    grassTilemap.SetTile(new Vector3Int(x, actualHeight, 0), grass);
                    GenerateDecorations(x, actualHeight + 1);
                }
                Debug.Log("--------------------");
            }
            

            

            
            
            if(x % 50 == 0 && x != 500 && x > 90)
            {
                Debug.Log("Heyyas" + x);
                GenerateEnemies(x, actualHeight);
            }
        }
        GeneratePlatforms(positions);
    }

    void GenerateDecorations(int x, int y)
    {
        int random = Random.Range(0, 20);
        if(random == 0)
        {
            decorations.SetTile(new Vector3Int(x, y, 0), shroom);
        } else if(random == 1)
        {
            decorations.SetTile(new Vector3Int(x, y, 0), crate);
        } else if(random <= 4)
        {
            decorations.SetTile(new Vector3Int(x, y, 0), rock);
        }
        else if(random <= 8)
        {
            decorations.SetTile(new Vector3Int(x, y, 0), grass2);
        }
        else if(random <= 12)
        {
            decorations.SetTile(new Vector3Int(x, y, 0), grass1);
        }
        /*
          decorations.SetTile(new Vector3Int(x, y, 0), branch1);
         decorations.SetTile(new Vector3Int(x, y, 0), branch2);
         */
        
    }

    void GenerateEnemies(int width, int height)
    {
        int value = 0;
        for(int x = 0; x < Random.Range(1, 4);  x++)
        {
            value = Random.Range(0, 3);
            switch(value)
            {
                case 0:
                    Instantiate(enemy1, (new Vector3(width + Random.Range(0, 10), height + 20, 0)), transform.rotation);
                    break;
                case 1:
                    Instantiate(enemy2, (new Vector3(width + Random.Range(0, 10), height + 20, 0)), transform.rotation);
                    break;
                case 2:
                    Instantiate(enemy3, (new Vector3(width + Random.Range(0, 10), height + 20, 0)), transform.rotation);
                    break;
                default:
                    break;

            }
        }
        
    }

    void GeneratePlatforms(List<Vector3Int> positions)
    {
        //int value = 0;
        for (int x = 0; x < positions.Count; x++)
        {
            if(grassTilemap.HasTile(new Vector3Int((int)positions[x].x, (int)positions[x].y -1, 0)) && positions[x].z != -2)
            {

            }
            else if (positions[x].z == 1 || positions[x].z == 2)
            {
                int k = 0;
                if ( 0 == Random.Range(0, 2) || positions[x].z == 2)
                {
                    platformTileMap.SetTile(new Vector3Int((int)positions[x].x, (int)positions[x].y, 0), platformStart);
                }
                

                for(int d =  1; d < 4; d++)
                {
                    k = x + d;
                    Debug.Log("haha" + k);
                    if (k < positions.Count)
                    {
                        //d = 4;
                        //Debug.Log("greg" + positions[k]);
                        if (positions[k].z == -1 && positions[k].y == positions[x].y)
                        {
                            positions[k] = new Vector3Int(positions[k].x, (int)positions[k].y, -2);
                            for (int l = positions[x].x + 1; l < positions[k].x; l++)
                            {
                                platformTileMap.SetTile(new Vector3Int(l, (int)positions[x].y, 0), platform);
                            }
                            platformTileMap.SetTile(new Vector3Int((int)positions[x].x, (int)positions[x].y, 0), platformStart);
                        }
                    }

                }
                
                
            }
            else if (positions[x].z == -1 || positions[x].z == -2)
            {
                if (positions[x].z == -1 && 0 == Random.Range(0, 2))
                {
                    platformTileMap.SetTile(new Vector3Int((int)positions[x].x, (int)positions[x].y, 0), platformEnd);
                }
                else if (positions[x].z == -2)
                {
                    platformTileMap.SetTile(new Vector3Int((int)positions[x].x, (int)positions[x].y, 0), platformEnd);
                }
                
            }
            else
            {
                /*Debug.Log("paparica");
                platformTileMap.SetTile(new Vector3Int((int)positions[x].x, (int)positions[x].y, 0), platform);*/
            }
            
            //platformTileMap.SetTile(positions[x], grass);
        }

    }

    public void StartGeneration(InputAction.CallbackContext context)
    {
        /*if (context.started)
        {
            seed = Random.Range(-1000000, 1000000);
            Generation();
        }*/
        
    }

    public void DeleteGeneration(InputAction.CallbackContext context)
    {
        /*if (context.started)
        {
            stoneTilemap.ClearAllTiles();
            dirtTilemap.ClearAllTiles();
            grassTilemap.ClearAllTiles();
        }*/
        
    }
}
