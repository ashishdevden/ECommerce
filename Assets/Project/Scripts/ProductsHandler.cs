using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductsHandler : MonoBehaviour
{
    public List<Product> products = new List<Product>();
    public static ProductsHandler instance;

    public List<Product> filteredProducts;

    public Category currentCategory;
    public Group currentGroup;
    public RecyclableScroll recyclableScroll;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);

        SetDefaultproductAsFilteredProducts();
        recyclableScroll.total = filteredProducts.Count;
        recyclableScroll.UpdateContent += UpdateContent;
    }

    private void Start()
    {
        /* products = new List<Product>();
         for (int i = 0; i < 3; i++)
         {
             for (int j = 0; j < 3; j++)
             {
                 for (int k = 0; k < 5; k++)
                 {
                     products.Add(new Product()
                     {
                         name = ((Category)i).ToString() + " " + ((Group)j).ToString() + " " + k.ToString(),
                         category = (Category)i,
                         group = (Group)j
                     });
                 }
             }
         }*/

    }

    public void ApplyFilter()
    {
        filteredProducts = new List<Product>();
        for (int i = 0; i < products.Count; i++)
        {
            if (products[i].category == currentCategory && products[i].group == currentGroup)
            {
                filteredProducts.Add(products[i]);
            }
        }

        recyclableScroll.total = filteredProducts.Count;
        recyclableScroll.ResetScroll();
    }

    public void ResetFilter()
    {
        SetDefaultproductAsFilteredProducts();
        recyclableScroll.total = filteredProducts.Count;
        recyclableScroll.ResetScroll();

    }



    public void UpdateContent(Item item, int filterProductIndex)
    {

        item.img.sprite = filteredProducts[filterProductIndex].img;
        item.text.text = filterProductIndex.ToString();
        item.text.text = filteredProducts[filterProductIndex].name;

    }

    public void SetDefaultproductAsFilteredProducts()
    {
        filteredProducts = new List<Product>();
        for (int i = 0; i < products.Count; i++)
        {
            filteredProducts.Add(products[i]);
        }
    }

    public void SetCatergory(int category)
    {
        currentCategory = (Category)category;
    }

    public void SetGroup(int group)
    {
        currentGroup = (Group)group;
    }
}


[System.Serializable]
public class Product
{
    public string name;
    public Sprite img;
    public Category category;
    public Group group;
}

public enum Category
{
    Watches, Cloths, Jewelry
}

public enum Group
{
    Male, Female, Kids
}