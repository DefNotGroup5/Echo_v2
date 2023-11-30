package via.sdj3.grpcserverexample.entities;

import jakarta.persistence.*;

@Entity
@Table
public class AdminEntity {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int id;

    @Column(nullable = false, unique = true)
    private String email;

    @Column
    private String firstName;

    @Column
    private String lastName;

    @Column(nullable = false)
    private String password;

    public AdminEntity() {}

    public AdminEntity(String email, String firstName, String lastName, String password) {
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.password = password;
    }

    public int getId()
    {
    return id;
    }
    
    public void setId(int id)
    {
    this.id = id;
    }

    public String getEmail()
    {
    return email;
    }
    
    public void setEmail(String email)
    {
    this.email = email;
    }

    public String getFirstName()
    {
    return firstName;
    }
    
    public void setFirstName(String firstName)
    {
    this.firstName = firstName;
    }

    public String getLastName()
    {
    return lastName;
    }
    
    public void setLastName(String lastName)
    {
    this.lastName = lastName;
    }

    public String getPassword()
    {
    return password;
    }
    
    public void setPassword(String password)
    {
    this.password = password;
    }
    
}
