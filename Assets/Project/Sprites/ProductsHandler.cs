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

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(this);
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
    }





}


[System.Serializable]
public class Product
{
    public string Name;
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