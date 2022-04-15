package com.example.O2MaM2O.controllers;

import com.example.O2MaM2O.models.Address;
import com.example.O2MaM2O.models.Person;
import com.example.O2MaM2O.repo.AddressRepo;
import com.example.O2MaM2O.repo.PersonRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class MainController {

    @Autowired
    AddressRepo addressRepo;
    @Autowired
    PersonRepo personRepo;

    @GetMapping("/")
    public String Index(Model model){
        Iterable<Person> persons = personRepo.findAll();
        model.addAttribute("persons", persons);
        Iterable<Address> addresses = addressRepo.findAll();
        model.addAttribute("addresses", addresses);

        return "index";
    }

    @PostMapping("/add")
    public String AddPerson(@RequestParam String name,
                            @RequestParam String street){
        Address address = addressRepo.findByStreet(street);
        Person person = new Person(name, address);
        personRepo.save(person);

        return "redirect:/";
    }
}
