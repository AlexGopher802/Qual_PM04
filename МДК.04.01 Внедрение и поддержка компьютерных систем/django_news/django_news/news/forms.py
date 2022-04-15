from django import forms
from .models import Post
from .models import Tag
from itertools import chain
from django.contrib.auth.models import User

class Comment_Form(forms.Form):
    comment = forms.CharField(label='', widget=forms.Textarea(attrs={'class':'form-control'}))

class Post_Form(forms.ModelForm):
    class Meta:
        model = Post
        fields = ['header', 'body', 'courses']
        
        # widgets = {
        #     'header': forms.TextInput(attrs={'class':'form-control'}),
        #     'body': forms.Textarea(attrs={'class':'form-control'}),
        # }

class Tag_Form(forms.ModelForm):
    class Meta:
        model = Tag
        fields = ['nameTag']

class UserUpdateForm(forms.ModelForm):
    class Meta:
        model = User
        fields = ['email', 'username', 'first_name', 'last_name']
        widgets = {
            'email' : forms.TextInput(attrs={'class':'form-control'}),
            'username' : forms.TextInput(attrs={'class':'form-control', 'readonly':''}),
            'first_name' : forms.TextInput(attrs={'class':'form-control'}),
            'last_name' : forms.TextInput(attrs={'class':'form-control'}),
        }
