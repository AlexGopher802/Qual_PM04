# Generated by Django 3.2.7 on 2021-11-22 15:24

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('news', '0005_alter_post_courses'),
    ]

    operations = [
        migrations.RemoveField(
            model_name='post',
            name='courses',
        ),
        migrations.AddField(
            model_name='tag',
            name='post',
            field=models.ManyToManyField(to='news.Post'),
        ),
    ]
