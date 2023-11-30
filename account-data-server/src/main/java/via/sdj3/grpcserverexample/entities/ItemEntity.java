package via.sdj3.grpcserverexample.entities;


import jakarta.persistence.*;

@Entity
@Table
public class ItemEntity {

    @Id
    @Column
    @GeneratedValue
    private int id;

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

    @Column
    private String seller;

    public ItemEntity(){ }

    public ItemEntity(int id, String name, String image_url, String description, int price, int quantity, int stock_available, String seller){
        this.id = id;
        this.name = name;
        this.image_url = image_url;
        this.description = description;
        this.price = price;
        this.quantity = quantity;
        this.stock_available = stock_available;
        this.seller = seller;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getImage_url() {
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

    public int getStock_available() {
        return stock_available;
    }

    public void setStock_available(int stock_available) {
        this.stock_available = stock_available;
    }

    public String getSeller(){
        return seller;
    }

    public void setSeller(String seller){
        this.seller =seller;
    }
}
