from django.contrib import admin
from .models import * 
from itertools import chain

class BooksAdmin(admin.ModelAdmin):  
    def tags(self, obj):
        a = obj.courses.values_list('nameTag')
        return list(chain.from_iterable(a))
    list_display = ('header', 'body', 'author', 'time_post', 'tags')  

admin.site.register(Post, BooksAdmin)
admin.site.register(Comment)
admin.site.register(Tag)