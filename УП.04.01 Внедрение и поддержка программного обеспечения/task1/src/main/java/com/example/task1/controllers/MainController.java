package com.example.task1.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

@Controller
public class MainController {
    @GetMapping("/")
    public String Index(){
        return "index";
    }

    @GetMapping("getcalculate")
    public String GetCalculate(@RequestParam(required = false) Integer aop,
                               @RequestParam(required = false) Integer bop,
                               @RequestParam(required = false) String operators,
                               Model model){

        String op = "";
        Integer result = 0;
        switch (operators){
            case "add":
                op = "+";
                result = aop + bop;
                break;
            case "dif":
                op = "-";
                result = aop - bop;
                break;
            case "mul":
                op = "*";
                result = aop * bop;
                break;
            case "div":
                op = "/";
                result = aop / bop;
                break;
        }

        model.addAttribute("result", String.format("%d %s %d = %d", aop, op, bop, result));
        return "index";
    }

    @PostMapping("postcalculate")
    public String PostCalculate(@RequestParam(required = false) Integer aop,
                                @RequestParam(required = false) Integer bop,
                                @RequestParam(required = false) String operators,
                                Model model){

        String op = "";
        Integer result = 0;
        switch (operators){
            case "add":
                op = "+";
                result = aop + bop;
                break;
            case "dif":
                op = "-";
                result = aop - bop;
                break;
            case "mul":
                op = "*";
                result = aop * bop;
                break;
            case "div":
                op = "/";
                result = aop / bop;
                break;
        }

        model.addAttribute("result", String.format("%d %s %d = %d", aop, op, bop, result));

        return "index";
    }
}
