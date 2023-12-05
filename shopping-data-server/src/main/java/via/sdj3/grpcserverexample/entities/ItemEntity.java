package via.sdj3.grpcserverexample.entities;

import jakarta.persistence.*;

@Entity
@Table
public class ItemEntity {

//    @Id
//    @Column
//    @GeneratedValue
//    private int sellerId;
//
//    @Id
//    @Column
//    @GeneratedValue
//    private int itemId;
    @EmbeddedId
    @Column
    private ItemEntityId id;

    @Column
    private String name;

    @Column
    private String image_url;

    @Column
    private String description;

    @Column
    private int price;

    @Column
    private int quantity;

    @Column
    private int stock_available;


    public ItemEntity(){ }

//    public ItemEntity(int sellerId, int itemId, String name, String image_url, String description, int price, int quantity, int stock_available){
//        this.sellerId = sellerId;
//        this.itemId = itemId;
//        this.name = name;
//        this.image_url = image_url;
//        this.description = description;
//        this.price = price;
//        this.quantity = quantity;
//        this.stock_available = stock_available;
//    }
//
//    public int getSellerId() {
//        return sellerId;
//    }
//
//    public void setSellerId(int sellerId) {
//        this.sellerId = sellerId;
//    }
//
//    public int getItemId(){ return itemId;  }
//
//    public void setItemId(int itemId) { this.itemId = itemId; }

    public ItemEntity(ItemEntityId id, String name, String image_url,
        String description, int price, int quantity, int stock_available)
    {
        this.id = id;
        this.name = name;
        this.image_url = image_url;
        this.description = description;
        this.price = price;
        this.quantity = quantity;
        this.stock_available = stock_available;
    }

    public ItemEntityId getId()
    {
        return id;
    }

    public void setId(ItemEntityId id)
    {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getImageUrl() {
        return image_url;
    }

    public void setImage_url(String image_url) {
        this.image_url = image_url;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public int getPrice() {
        return price;
    }

    public void setPrice(int price) {
        this.price = price;
    }

    public int getQuantity(){
        return quantity;
    }

    public void setQuantity(int quantity){
        this.quantity = quantity;
    }

    public int getStock() {
        return stock_available;
    }

    public void setStock_available(int stock_available) {
        this.stock_available = stock_available;
    }


}

