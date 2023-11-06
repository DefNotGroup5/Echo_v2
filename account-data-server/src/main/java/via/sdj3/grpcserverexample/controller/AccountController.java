package via.sdj3.grpcserverexample.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.UserRepository;

@CrossOrigin(origins = "http://localhost:8081")
@RestController
@RequestMapping("/api")
public class AccountController {

    //auto wired is to inject the repository into a local variable
    @Autowired
    UserRepository userRepository;

    //can change later the /user part
    @PostMapping("/user")

    public ResponseEntity<UserEntity> createAccount(@RequestBody UserEntity userEntity) {
        try {
            UserEntity user = userRepository
                    .save(new UserEntity(userEntity.getId(), userEntity.getEmail(), userEntity.getFirstName(), userEntity.getLastName(), userEntity.getPassword(), userEntity.getAddress(), userEntity.getCity(), userEntity.getPostalCode(), userEntity.getCountry(), userEntity.isSeller()));
            return new ResponseEntity<>(user, HttpStatus.CREATED);
        } catch (Exception e) {
            return new ResponseEntity<>(null, HttpStatus.INTERNAL_SERVER_ERROR);
        }
    }


}
