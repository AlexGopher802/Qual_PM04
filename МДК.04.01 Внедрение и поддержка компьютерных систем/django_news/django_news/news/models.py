from django.db import models
from django.shortcuts import reverse

class Post(models.Model):
    header = models.CharField(max_length=200, verbose_name='Заголовок статьи')
    body = models.TextField(max_length=500, verbose_name='Текст статьи')   
    author = models.CharField(max_length=50, verbose_name='Автор статьи')
    time_post = models.DateTimeField(auto_now=True, verbose_name='Дата публикации статьи')
    courses = models.ManyToManyField('Tag', blank=True, related_name='posts')

    def __str__(self):
        return '{}'.format(self.header)

    def get_absolute_url(self):
        return reverse("post_detail_url", kwargs={"post_id": self.pk})
    
    def get_update_url(self):
        return reverse("post_update_url", kwargs={"id_post": self.pk})
    
    def get_delete_url(self):
        return reverse("post_delete_url", kwargs={"id_post": self.pk})
    
    class Meta:
        verbose_name = 'Статья'
        verbose_name_plural = "Статьи"


class Tag(models.Model):
    nameTag = models.TextField(max_length=500, verbose_name='Текст тега')
    
    def __str__(self):
        return self.nameTag

class Comment(models.Model):
    comment = models.TextField(max_length=500, verbose_name='Текст комментария')
    author = models.CharField(max_length=50, verbose_name='Автор комментария')
    time_post = models.DateTimeField(auto_now=True, verbose_name='Дата публикации комментария')
    fk_post = models.ForeignKey(to = Post, on_delete = models.SET_NULL, blank=True, null=True)

    def __str__(self):
        return '{}, {}, {}'.format(self.fk_post, self.author, self.time_post)

    class Meta:
        verbose_name = 'Коммантарий'
        verbose_name_plural = "Комментарии"
        permissions = (("can_see_author", "Можно видеть автора комментария."),)

